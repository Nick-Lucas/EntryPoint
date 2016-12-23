using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    internal static class ArgumentArrayExtensions {
        // Determines if a given arg array element is a - option
        public static bool IsSingleDash(this string arg) {
            return arg.StartsWith(EntryPointApi.DASH_SINGLE)
               && !arg.StartsWith(EntryPointApi.DASH_DOUBLE);
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
