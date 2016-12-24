using EntryPoint.Exceptions;
using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    internal static class ArgumentArrayExtensions {
        public static List<Token> SplitSingleOptions(this Token arg) {
            if (!arg.IsSingleDashOption()) {
                throw new InvalidOperationException(
                    $"arg.{nameof(SplitSingleOptions)}() should only be used with Single dash args");
            }

            return arg
                .Value
                .Trim(EntryPointApi.DASH_SINGLE.ToCharArray())
                .ToCharArray()
                .Select(c => new Token(EntryPointApi.DASH_SINGLE + c, true))
                .ToList();
        }

        // Determines if a given arg array element is a - option
        public static bool IsSingleDashOption(this Token arg) {
            return arg.IsOption 
                && arg.Value.StartsWith(EntryPointApi.DASH_SINGLE)
               && !arg.Value.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static bool IsDoubleDashOption(this Token arg) {
            return arg.IsOption 
                && arg.Value.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static List<Token> GetSingleDashOptions(this List<Token> args) {
            var singles = args
                // Get all single dash options
                .Where(a => a.IsSingleDashOption());
            return singles.ToList();
        }

        public static List<Token> GetDoubleDashOptions(this List<Token> args) {
            var doubles = args
                // Get all double dash options
                .Where(a => a.IsDoubleDashOption());
            return doubles.ToList();
        }

        public static ModelOption GetOption(this Token arg, Model model) {
            var option = model.FirstOrDefault(o => {
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

        // Returns the array index of a given - option, or -1
        public static int SingleDashIndex(this List<Token> args, char argName) {
            return args.FindIndex(s => 
                   s.IsSingleDashOption() 
                && s.Value.Contains(argName));
        }

        // Returns the array index of a given -- option, or -1
        public static int DoubleDashIndex(this List<Token> args, string argName) {
            return args.FindIndex(s =>
                       s.IsDoubleDashOption()
                    && s.Value.StartsWith(EntryPointApi.DASH_DOUBLE + argName, StringComparison.CurrentCultureIgnoreCase));
        }

        // Determines if an option is used at all in the arguments list
        public static bool OptionExists(this List<Token> args, BaseOptionAttribute option) {
            return option.SingleDashIndex(args) >= 0
                || option.DoubleDashIndex(args) >= 0;
        }
    }
}
