using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;
using EntryPoint.Exceptions;
using EntryPoint.Parsing;

namespace EntryPoint.OptionModel {
    internal class Model {
        public BaseApplicationOptions ApplicationOptions { get; private set; }

        internal Model(BaseApplicationOptions applicationOptions) {
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
        // TODO: break away domain logic into helper class
        public ModelOption FindOptionByToken(Token token) {
            var option = this.Options.FirstOrDefault(o => {
                return ((token.IsSingleDashOption() && token.Value.Contains(o.Definition.ShortName))
                    || (token.IsDoubleDashOption() && token.Value.StartsWith(
                            EntryPointApi.DASH_DOUBLE + o.Definition.LongName,
                            StringComparison.CurrentCultureIgnoreCase)));
            });

            if (option == null) {
                throw new UnkownOptionException(
                    $"The option {token.Value} was not recognised. "
                    + "Please ensure all given arguments are valid. Try --help");
            }

            return option;
        }

        // TODO: break away domain logic into helper class
        public List<ModelOption> WhereOptionNotIn(List<TokenGroup> tokenGroups) {
            return this.Options.Where(mo => !tokenGroups.Any(tg => {
                return tg.Option.Value.Equals(EntryPointApi.DASH_SINGLE + mo.Definition.ShortName, StringComparison.CurrentCulture)
                    || tg.Option.Value.Equals(EntryPointApi.DASH_DOUBLE + mo.Definition.LongName, StringComparison.CurrentCultureIgnoreCase);
            })).ToList();
        }

        // TODO: break away domain logic into validation class
        // Check model contains only unique names
        public void ValidateNoDuplicateOptionNames() {

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
                $"The given {nameof(BaseApplicationOptions)} implementation was invalid. "
                + $"There are duplicate single dash arguments: {String.Join("/", duplicateOptionNames)}");
        }
    }
}
