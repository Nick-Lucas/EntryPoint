using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;

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
                ValidateRequiredOption(prop, option, args);

                object value = option.OptionParser.GetValue(args, prop.PropertyType, option);
                prop.SetValue(argumentsModel, value);
            }

            return argumentsModel;
        }

        // If a property has a Required attribute, enforce the requirement
        static void ValidateRequiredOption(PropertyInfo prop, BaseOptionAttribute option, string[] args) {
            var required = prop.GetCustomAttribute<OptionRequiredAttribute>();
            if (required == null) {
                return;
            }

            if (!args.OptionExists(option)) {
                throw new OptionRequiredException(
                    $"The option {EntryPointApi.DASH_SINGLE}{option.SingleDashChar}/"
                    + $"{EntryPointApi.DASH_DOUBLE}{option.DoubleDashName} " 
                    + "was not included, but is a required option");
            }
        }
    }
}
