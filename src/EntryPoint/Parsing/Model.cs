using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;
using EntryPoint.Exceptions;

namespace EntryPoint.Parsing {
    internal class Model : List<ModelOption> {
        internal Model(BaseArgumentsModel argumentsModel) {
            var properties = argumentsModel.GetType().GetRuntimeProperties();
            var options = properties
                .Where(prop => prop.GetOptionAttribute() != null)
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

        public List<ModelOption> WhereNotIn(List<TokenGroup> tokenGroups) {
            return this.Where(mo => !tokenGroups.Any(tg => {
                return tg.OptionToken.Value.Equals(EntryPointApi.DASH_SINGLE + mo.Definition.SingleDashChar, StringComparison.CurrentCulture)
                    || tg.OptionToken.Value.Equals(EntryPointApi.DASH_DOUBLE + mo.Definition.DoubleDashName, StringComparison.CurrentCultureIgnoreCase);
            })).ToList();
        }
    }
}
