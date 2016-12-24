using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    internal static class ArgumentArrayExtensions {
        public static List<Token> GetSingleArgs(this Token arg) {
            if (!arg.IsSingleDash()) {
                throw new InvalidOperationException(
                    $"arg.{nameof(GetSingleArgs)}() should only be used with Single dash args");
            }

            return arg
                .Value
                .Trim(EntryPointApi.DASH_SINGLE.ToCharArray())
                .ToCharArray()
                .Select(c => new Token(EntryPointApi.DASH_SINGLE + c, true))
                .ToList();
        }

        // Determines if a given arg array element is a - option
        public static bool IsSingleDash(this Token arg) {
            return arg.IsOption 
                && arg.Value.StartsWith(EntryPointApi.DASH_SINGLE)
               && !arg.Value.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static bool IsDoubleDash(this Token arg) {
            return arg.IsOption 
                && arg.Value.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static List<Token> FlattenSingles(this List<Token> args) {
            var singles = args
                // Get all single dash options
                .Where(a => a.IsSingleDash())

                // Get all args in the form -o
                .SelectMany(s => s.GetSingleArgs());
            return singles.ToList();
        }

        public static List<Token> FlattenDoubles(this List<Token> args) {
            var doubles = args
                // Get all double dash options
                .Where(a => a.IsDoubleDash());
            return doubles.ToList();
        }

        public static BaseOptionAttribute GetOption(this Token arg, List<BaseOptionAttribute> options) {
            return options.FirstOrDefault(o => {
                return arg.IsOption && 
                      ((arg.IsSingleDash() && arg.Value.Contains(o.SingleDashChar))
                    || (arg.IsDoubleDash() && arg.Value.StartsWith(
                                                    EntryPointApi.DASH_DOUBLE + o.DoubleDashName, 
                                                    StringComparison.CurrentCultureIgnoreCase)));
            });
        }

        // Returns the array index of a given - option, or -1
        public static int SingleDashIndex(this List<Token> args, char argName) {
            return args.FindIndex(s => 
                   s.IsSingleDash() 
                && s.Value.Contains(argName));
        }

        // Returns the array index of a given -- option, or -1
        public static int DoubleDashIndex(this List<Token> args, string argName) {
            return args.FindIndex(s =>
                       s.IsDoubleDash()
                    && s.Value.StartsWith(EntryPointApi.DASH_DOUBLE + argName, StringComparison.CurrentCultureIgnoreCase));
        }

        // Determines if an option is used at all in the arguments list
        public static bool OptionExists(this List<Token> args, BaseOptionAttribute option) {
            return option.SingleDashIndex(args) >= 0
                || option.DoubleDashIndex(args) >= 0;
        }
    }
}
