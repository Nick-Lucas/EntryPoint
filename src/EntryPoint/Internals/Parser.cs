using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Internals;

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
            ValidateModelForDuplicates(argumentsModel, options);
            ValidateArgumentsForDuplicates(args, options);
            ValidateUnknownOption(args, options);
            PopulateOperands(argumentsModel, options, args);

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

        static void ValidateArgumentsForDuplicates(string[] args, List<BaseOptionAttribute> options) {
            var map = args.FlattenSingles().Select(a => a.GetOption(options)).ToList();
            map.AddRange(args.FlattenDoubles().Select(a => a.GetOption(options)));
            var duplicates = map.Duplicates(new BaseOptionAttributeEqualityComparer());
            if (duplicates.Any()) {
                throw new DuplicateOptionException(
                    $"Duplicate options were entered for " 
                    + $"${string.Join("/", duplicates.Select(o => o.DoubleDashName))}");
            }
        }

        static void ValidateUnknownOption(string[] args, List<BaseOptionAttribute> options) {
            // Validate shortfort Options
            foreach (var arg in args.FlattenSingles()) {
                if (arg.GetOption(options) == null) {
                    AssertUnkownOption(arg);
                }
            }

            // Validate full Options
            foreach (var arg in args.FlattenDoubles()) {
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

        static void ValidateModelForDuplicates(BaseArgumentsModel model, List<BaseOptionAttribute> options) {
            // Check the single dash options
            var singleDups = options
                .Where(o => o.SingleDashChar > char.MinValue)
                .Select(o => o.SingleDashChar.ToString())
                .Duplicates(StringComparer.CurrentCulture)
                .ToList();
            if (singleDups.Any()) {
                AssertDuplicateOptionsInModel(model, singleDups);
            }
            // Check the double dash options
            var doubleDups = options
                .Where(o => o.DoubleDashName != string.Empty)
                .Select(o => o.DoubleDashName)
                .Duplicates(StringComparer.CurrentCultureIgnoreCase)
                .ToList();
            if (doubleDups.Any()) {
                AssertDuplicateOptionsInModel(model, doubleDups);
            }
        }
        static void AssertDuplicateOptionsInModel(BaseArgumentsModel model, List<string> options) {
            throw new InvalidModelException(
                $"The given model {model.GetType().Name} was invalid. "
                + $"There are duplicate single dash arguments: {String.Join("/", options)}");
        }

        static void PopulateOperands(
            BaseArgumentsModel argumentsModel, List<BaseOptionAttribute> options, string[] args) {

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
