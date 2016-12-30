using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryPoint.Arguments;

namespace EntryPoint.Help {
    internal static class HelpRules {
        public const string HelpLong = "help";
        public const char HelpShort = 'h';
        public const string HelpShortString = "h";

        public static bool InvokedByArgument(string arg) {
            if (arg == null) {
                return false;
            }
            arg = arg.Trim(Cli.DASH_SINGLE.ToCharArray());
            return HelpLong.Equals(arg, StringComparison.CurrentCultureIgnoreCase)
                || HelpShortString.Equals(arg, StringComparison.CurrentCulture);
        }
    }
}
