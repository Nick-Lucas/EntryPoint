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

        internal Model(BaseApplicationOptions argumentsModel) {
            ApplicationOptions = argumentsModel;

            var properties = argumentsModel.GetType().GetRuntimeProperties();
            var options = properties
                .Where(prop => prop.GetOptionDefinition() != null)
                .Select(prop => new ModelOption(prop))
                .ToList();
            base.AddRange(options);
        }

        // Find the ModelOption for the given Token, or null
        // TODO: break away domain logic into helper class
        public ModelOption FindByToken(Token arg) {
            var option = this.FirstOrDefault(o => {
                return ((arg.IsSingleDashOption() && arg.Value.Contains(o.Definition.SingleDashChar))
                    || (arg.IsDoubleDashOption() && arg.Value.StartsWith(
                            EntryPointApi.DASH_DOUBLE + o.Definition.DoubleDashName,
                            StringComparison.CurrentCultureIgnoreCase)));
            });

            if (option == null) {
                throw new UnkownOptionException(
                    $"The option {arg.Value} was not recognised. "
                    + "Please ensure all given arguments are valid. Try --help");
            }

            return option;
        }

        // TODO: break away domain logic into helper class
        public List<ModelOption> WhereNotIn(List<TokenGroup> tokenGroups) {
            return this.Where(mo => !tokenGroups.Any(tg => {
                return tg.OptionToken.Value.Equals(EntryPointApi.DASH_SINGLE + mo.Definition.SingleDashChar, StringComparison.CurrentCulture)
                    || tg.OptionToken.Value.Equals(EntryPointApi.DASH_DOUBLE + mo.Definition.DoubleDashName, StringComparison.CurrentCultureIgnoreCase);
            })).ToList();
        }

        public void ValidateNoDuplicateNames() {
            // Check the single dash options
            var singleDups = this
                .Where(o => o.Definition.SingleDashChar > char.MinValue)
                .Select(o => o.Definition.SingleDashChar.ToString())
                .Duplicates(StringComparer.CurrentCulture)
                .ToList();
            if (singleDups.Any()) {
                AssertDuplicateOptionsInModel(singleDups);
            }
            // Check the double dash options
            var doubleDups = this
                .Where(o => o.Definition.DoubleDashName != string.Empty)
                .Select(o => o.Definition.DoubleDashName)
                .Duplicates(StringComparer.CurrentCultureIgnoreCase)
                .ToList();
            if (doubleDups.Any()) {
                AssertDuplicateOptionsInModel(doubleDups);
            }
        }
        static void AssertDuplicateOptionsInModel(List<string> options) {
            throw new InvalidModelException(
                $"The given {nameof(BaseApplicationOptions)} implementation was invalid. "
                + $"There are duplicate single dash arguments: {String.Join("/", options)}");
        }
    }
}
