using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;
using EntryPoint.Exceptions;
using EntryPoint.Parsing;

namespace EntryPoint.Arguments {
    internal class Model {
        public BaseCliArguments ApplicationOptions { get; private set; }

        internal Model(BaseCliArguments applicationOptions) {
            ApplicationOptions = applicationOptions;

            // TODO: extract this reflection logic
            var properties = applicationOptions.GetType().GetRuntimeProperties();

            // Map Options Model
            Options = properties
                .Where(prop => prop.GetOptionDefinition() != null)
                .Select(prop => new ModelOption(prop))
                .ToList();

            // Map Operands Model
            Operands = properties
                .Where(prop => prop.GetOperandDefinition() != null)
                .Select(prop => new ModelOperand(prop))
                .ToList();

            Help = applicationOptions.GetType().GetTypeInfo().GetHelp();
        }

        // Help attribute applied to the class itself
        public HelpAttribute Help { get; private set; }

        // Options defined by the class
        public List<ModelOption> Options { get; set; }

        // Operands defined by the class
        public List<ModelOperand> Operands { get; set; }

        // Find the ModelOption for the given Token, or null
        public ModelOption FindOptionByToken(Token token) {
            var option = this.Options
                .FirstOrDefault(o => token.InvokesOption(o));
            if (option == null) {
                throw new UnkownOptionException(
                    $"The option {token.Value} was not recognised. "
                    + "Please ensure all given arguments are valid. Try --help");
            }
            return option;
        }

        public List<ModelOption> WhereOptionsNotIn(List<TokenGroup> tokenGroups) {
            return this.Options
                .Where(o => !tokenGroups.Any(tg => tg.Option.InvokesOption(o)))
                .ToList();
        }


        // ** Model Validation **

        public void Validate() {
            ValidateNoDuplicateOptionNames();
            ValidateContigiousOperandMapping();
        }

        // Check that operand positions have the form: [ 1, 2, 3, 4 ](contigious)
        // As opposed to [ 0, 1, 2, 3 ](less than 1) OR [ 1, 2, 4, 5 ](gaps)
        void ValidateContigiousOperandMapping() {
            int lastPosition = 0;
            var operands = Operands.OrderBy(o => o.Definition.Position);

            foreach (var operand in operands) {
                int position = operand.Definition.Position;
                int diff = position - lastPosition;

                // Position should always be greater than 1
                if (position < 1) {
                    throw new InvalidModelException("Negative Operand Position. "
                        + $"Operand(position: {position}) on "
                        + $"{operand.Property.Name} was < 1. The first position is 1");

                // Position should always increase by 1
                } else if (diff != 1) {
                    throw new InvalidModelException("Non-contiguous Operand Positions. "
                        + $"Operand positions should cover every position from min->max. {position} and {lastPosition} were next to each other");
                }

                lastPosition = position;
            }
        }

        // Check model contains only unique names
        void ValidateNoDuplicateOptionNames() {

            // Check the single dash options
            var singleDashes = this.Options
                .Where(o => o.Definition.ShortName > char.MinValue)
                .Select(o => o.Definition.ShortName.ToString())
                .Duplicates(StringComparer.CurrentCulture)
                .ToList();
            if (singleDashes.Any()) {
                AssertDuplicateOptionsInModel(singleDashes);
            }

            // Check the double dash options
            var doubleDashes = this.Options
                .Where(o => o.Definition.LongName != string.Empty)
                .Select(o => o.Definition.LongName)
                .Duplicates(StringComparer.CurrentCultureIgnoreCase)
                .ToList();
            if (doubleDashes.Any()) {
                AssertDuplicateOptionsInModel(doubleDashes);
            }
        }
        static void AssertDuplicateOptionsInModel(List<string> duplicateOptionNames) {
            throw new InvalidModelException(
                $"The given {nameof(BaseCliArguments)} implementation was invalid. "
                + $"There are duplicate single dash arguments: {String.Join("/", duplicateOptionNames)}");
        }
    }
}
