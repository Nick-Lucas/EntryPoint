using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;
using EntryPoint.Exceptions;

namespace EntryPoint.Parsing {
    internal class Model : List<ModelOption> {
        public BaseApplicationOptions ApplicationOptions { get; private set; }

        internal Model(BaseApplicationOptions applicationOptions) {
            ApplicationOptions = applicationOptions;

            var properties = applicationOptions.GetType().GetRuntimeProperties();
            var options = properties
                .Where(prop => prop.GetOptionDefinition() != null)
                .Select(prop => new ModelOption(prop))
                .ToList();
            base.AddRange(options);
        }

        // Find the ModelOption for the given Token, or null
        // TODO: break away domain logic into helper class
        public ModelOption FindByToken(Token token) {
            var option = this.FirstOrDefault(o => {
                return ((token.IsSingleDashOption() && token.Value.Contains(o.Definition.SingleDashChar))
                    || (token.IsDoubleDashOption() && token.Value.StartsWith(
                            EntryPointApi.DASH_DOUBLE + o.Definition.DoubleDashName,
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
        public List<ModelOption> WhereNotIn(List<TokenGroup> tokenGroups) {
            return this.Where(mo => !tokenGroups.Any(tg => {
                return tg.Option.Value.Equals(EntryPointApi.DASH_SINGLE + mo.Definition.SingleDashChar, StringComparison.CurrentCulture)
                    || tg.Option.Value.Equals(EntryPointApi.DASH_DOUBLE + mo.Definition.DoubleDashName, StringComparison.CurrentCultureIgnoreCase);
            })).ToList();
        }

        // Check model contains only unique names
        public void ValidateNoDuplicateNames() {

            // Check the single dash options
            var singleDashes = this
                .Where(o => o.Definition.SingleDashChar > char.MinValue)
                .Select(o => o.Definition.SingleDashChar.ToString())
                .Duplicates(StringComparer.CurrentCulture)
                .ToList();
            if (singleDashes.Any()) {
                AssertDuplicateOptionsInModel(singleDashes);
            }

            // Check the double dash options
            var doubleDashes = this
                .Where(o => o.Definition.DoubleDashName != string.Empty)
                .Select(o => o.Definition.DoubleDashName)
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
