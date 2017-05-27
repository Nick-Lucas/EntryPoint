using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Common;
using EntryPoint.Exceptions;
using EntryPoint.Parsing;
using EntryPoint.Help;
using System.Reflection;

namespace EntryPoint.Arguments {

    internal class ArgumentModel {
        internal ArgumentModel(BaseCliArguments cliArguments) {
            CliArguments = cliArguments;
            Options = cliArguments.GetOptions();
            Operands = cliArguments.GetOperands();
            EnvironmentVariables = cliArguments.GetEnvironmentVariables();
            Help = cliArguments.GetHelpAttribute();
            HelpFacade = new HelpFacade(cliArguments);
        }

        public BaseCliArguments CliArguments { get; private set; }

        // Help attribute applied to the class itself
        public HelpAttribute Help { get; private set; }

        // Options defined by the class
        public List<Option> Options { get; set; }

        // Operands defined by the class
        public List<Operand> Operands { get; set; }

        // Environment Variables defined by the class
        public List<EnvironmentVariable> EnvironmentVariables { get; set; }

        // Facade for invoking Help
        public HelpFacade HelpFacade { get; set; }


        // ** Helpers **

        // Find the ModelOption for the given Token, or null
        public Option FindOptionByToken(Token token) {
            var option = this.Options
                .FirstOrDefault(o => token.InvokesOption(o));
            if (option == null) {
                throw new UnknownOptionException(
                    $"The option {token.Value} was not recognised. "
                    + "Please ensure all given arguments are valid. Try --help");
            }
            return option;
        }

        public List<Option> WhereOptionsNotIn(List<TokenGroup> tokenGroups) {
            return this.Options
                .Where(o => !tokenGroups.Any(tg => tg.Option.InvokesOption(o)))
                .ToList();
        }


        // ** Model Validation **

        public void Validate() {
            ValidateNoDuplicateOptionNames();
            ValidateContigiousOperandMapping();
            ValidateTypes();
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
                    throw new InvalidModelException("Missing Operand Position(s). "
                        + $"Operand positions should always increment by 1 from first->last. Positions {position} and {lastPosition} leave a gap");
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
                .Duplicates(StringComparer.CurrentCulture);
            if (singleDashes.Any()) {
                AssertDuplicateOptionsInModel(singleDashes);
            }

            // Check the double dash options
            var doubleDashes = this.Options
                .Where(o => o.Definition.LongName.HasValue())
                .Select(o => o.Definition.LongName)
                .Duplicates(StringComparer.CurrentCultureIgnoreCase);
            if (doubleDashes.Any()) {
                AssertDuplicateOptionsInModel(doubleDashes);
            }
        }
        static void AssertDuplicateOptionsInModel(List<string> duplicateOptionNames) {
            throw new InvalidModelException(
                $"There are duplicate option names: `{String.Join("/", duplicateOptionNames)}`");
        }

        void ValidateTypes() {
            foreach (var option in Options) {
                ValidatePropertyType(option.Property);
            }
            foreach (var operand in Operands) {
                ValidatePropertyType(operand.Property);
            }
        }
        void ValidatePropertyType(PropertyInfo property) {
            if (property.PropertyType.IsList()) {
                var listTypeArg = property.PropertyType.GenericTypeArguments[0];
                ValidateTypeIsSupported(listTypeArg);
            } else {
                ValidateTypeIsSupported(property.PropertyType);
            }
        }
        void ValidateTypeIsSupported(Type type) {
            if (type.GetTypeInfo().IsPrimitive) {
                return;
            }
            if (type.GetTypeInfo().IsEnum) {
                return;
            }
            if (type == typeof(string)) {
                return;
            }
            if (type == typeof(decimal)) {
                return;
            }
            if (type.IsNullable()) {
                ValidateTypeIsSupported(Nullable.GetUnderlyingType(type));
                return;
            }
            AssertTypeNotSupported(type);
        }
        void AssertTypeNotSupported(Type type) {
            throw new InvalidModelException(
                $"The type `{type.Name}` is not currently supported as an Option/Operand type");
        }
    }

}
