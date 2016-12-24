using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    internal static class ArgumentArrayExtensions {
        public static string[] GetSingleArgs(this string arg) {
            if (!arg.IsSingleDash()) {
                throw new InvalidOperationException(
                    $"arg.{nameof(GetSingleArgs)}() should only be used with Single dash args");
            }

            return arg
                .Trim(EntryPointApi.DASH_SINGLE.ToCharArray())
                .ToCharArray()
                .Select(c => EntryPointApi.DASH_SINGLE + c)
                .ToArray();
        }

        // Determines if a given arg array element is a - option
        public static bool IsSingleDash(this string arg) {
            return arg.StartsWith(EntryPointApi.DASH_SINGLE)
               && !arg.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static bool IsDoubleDash(this string arg) {
            return arg.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static List<string> FlattenSingles(this string[] args) {
            var singles = args
                // Get all single dash options
                .Where(a => a.IsSingleDash())

                // Get all args in the form -o
                .SelectMany(s => s.GetSingleArgs());
            return singles.ToList();
        }

        public static List<string> FlattenDoubles(this string[] args) {
            var doubles = args
                // Get all double dash options
                .Where(a => a.IsDoubleDash())

                // Trim off any parameters from options
                .Select(s => string.Concat(s.TakeWhile(c => c != '=')));
            return doubles.ToList();
        }

        public static BaseOptionAttribute GetOption(this string arg, List<BaseOptionAttribute> options) {
            return options.FirstOrDefault(o => {
                return (arg.IsSingleDash() && arg.Contains(o.SingleDashChar))
                    || (arg.IsDoubleDash() && arg.StartsWith(
                                                    EntryPointApi.DASH_DOUBLE + o.DoubleDashName, 
                                                    StringComparison.CurrentCultureIgnoreCase));
            });
        }

        // Returns the array index of a given - option, or -1
        public static int SingleDashIndex(this string[] args, char argName) {
            return Array.FindIndex(args, s => 
                   s.IsSingleDash() 
                && s.Contains(argName));
        }

        // Returns the array index of a given -- option, or -1
        public static int DoubleDashIndex(this string[] args, string argName) {
            return Array.FindIndex(args, s =>
                    s.StartsWith(EntryPointApi.DASH_DOUBLE + argName, StringComparison.CurrentCultureIgnoreCase));
        }

        // Determines if an option is used at all in the arguments list
        public static bool OptionExists(this string[] args, BaseOptionAttribute option) {
            return option.SingleDashIndex(args) >= 0
                || option.DoubleDashIndex(args) >= 0;
        }
    }
}
