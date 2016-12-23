using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;

namespace EntryPoint.Internals {
    internal static class Parser {

        // Takes the input from the API and orchestrates the process of population
        public static A ParseAttributes<A>(A argumentsModel, string[] args) where A : BaseArgumentsModel {
            var options = new List<BaseOptionAttribute>();
            var properties = argumentsModel.GetType().GetRuntimeProperties();
            foreach (var prop in properties) {
                var option = prop.GetCustomAttribute<BaseOptionAttribute>();
                if (option == null) {
                    continue;
                }
                options.Add(option);
                ValidateRequiredOption(prop, option, args);

                object value = option.OptionParser.GetValue(args, prop.PropertyType, option);
                prop.SetValue(argumentsModel, value);
            }
            ValidateUnknownOption(args, options);
            PopulateOperands(ref argumentsModel, options, args);

            return argumentsModel;
        }

        // If a property has a Required attribute, enforce the requirement
        static void ValidateRequiredOption(PropertyInfo prop, BaseOptionAttribute option, string[] args) {
            var required = prop.GetCustomAttribute<OptionRequiredAttribute>() != null;
            if (required && !args.OptionExists(option)) {
                throw new OptionRequiredException(
                    $"The option {EntryPointApi.DASH_SINGLE}{option.SingleDashChar}/"
                    + $"{EntryPointApi.DASH_DOUBLE}{option.DoubleDashName} "
                    + "was not included, but is a required option");
            }
        }

        static void ValidateUnknownOption(string[] args, List<BaseOptionAttribute> options) {
            // Validate shortfort Options
            var singles = args.Where(a => a.IsSingleDash());
            foreach (var arg in singles) {
                var singleArgs = arg.GetSingleArgs();
                foreach (var sArg in singleArgs) {
                    if (sArg.GetOption(options) == null) {
                        AssertUnkownOption(sArg);
                    }
                }
            }

            // Validate full Options
            var doubles = args.Where(a => a.IsDoubleDash());
            foreach (var arg in doubles) {
                if (arg.GetOption(options) == null) {
                    AssertUnkownOption(arg);
                }
            }
        }
        static void AssertUnkownOption(string arg) {
            throw new UnkownOptionException(
                $"The option {EntryPointApi.DASH_SINGLE}{arg} was not recognised. " 
                + "Please ensure all given arguments are valid. Try --help");
        }

        static void PopulateOperands<A>(ref A argumentsModel, List<BaseOptionAttribute> options, string[] args) 
            where A : BaseArgumentsModel {
            if (!args.Any()) {
                argumentsModel.Operands = new string[] { };
                return;
            }
            
            // Find the last item in the list which is a declared Option
            int lastIndex = args.Length;
            foreach (var arg in args.Reverse()) {
                var option = arg.GetOption(options);
                if (option != null) {
                    if (option is OptionParameterAttribute && !arg.Contains("=")) {
                        // If it's a parameter and not combined, the last option is 1 index further on
                        ++lastIndex;
                    }
                    break;
                }

                --lastIndex;
            }

            argumentsModel.Operands = args.Skip(lastIndex).ToArray();
        }
    }
}
