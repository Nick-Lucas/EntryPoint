using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;

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
    }
}
