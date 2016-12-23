using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.ArgumentTypeParsers;

namespace EntryPoint.Internals {
    internal static class ArgumentParserFactory {
        public static IArgumentType Switch {
            get {
                return new SwitchParser();
            }
        }
        public static IArgumentType Parameter {
            get {
                return new ParameterParser();
            }
        }
    }
}
