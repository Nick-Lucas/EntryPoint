using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace EntryPoint.Internals {
    internal static class Parser {

        // Takes the input from the API and orchestrates the process of population
        public static A ParseAttributes<A>(A argumentsModel, string[] args) {
            var properties = argumentsModel.GetType().GetRuntimeProperties();
            foreach (var prop in properties) {
                var option = prop.GetCustomAttribute<BaseOptionAttribute>();
                if (option == null) {
                    continue;
                }

                object value = option.OptionParser.GetValue(args, prop.PropertyType, option);
                prop.SetValue(argumentsModel, value);
            }

            return argumentsModel;
        }

    }
}
