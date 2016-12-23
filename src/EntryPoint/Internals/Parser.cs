using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace EntryPoint.Internals {
    internal static class Parser {
        public static A ParseAttributes<A>(A argumentsModel, string[] args) {
            var properties = argumentsModel.GetType().GetRuntimeProperties();
            foreach (var prop in properties) {
                var argDefinition = prop.GetCustomAttribute<BaseArgumentAttribute>();
                if (argDefinition == null) {
                    continue;
                }

                // TODO; make this more type safe and robust
                string value = argDefinition.OptionParser.GetValue(args, argDefinition);
                var changedType = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(argumentsModel, changedType);
            }

            return argumentsModel;
        }
    }
}
