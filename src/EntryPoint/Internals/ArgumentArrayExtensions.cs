using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    internal static class ArgumentArrayExtensions {
        public static bool IsSingleDash(this string arg) {
            return arg.StartsWith(EntryPointApi.DASH_SINGLE)
               && !arg.StartsWith(EntryPointApi.DASH_DOUBLE);
        }

        public static int SingleDashIndex(this string[] args, char argName) {
            return Array.FindIndex(args, s => 
                   s.IsSingleDash() 
                && s.Contains(argName)); //todo: make this case insensitive
        }

        public static int DoubleDashIndex(this string[] args, string argName) {
            return Array.FindIndex(args, s =>
                    s.StartsWith(EntryPointApi.DASH_DOUBLE + argName)); //todo: make this case insensitive
        }
    }
}
