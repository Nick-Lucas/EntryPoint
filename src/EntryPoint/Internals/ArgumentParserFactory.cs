using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.ArgumentTypeParsers;

namespace EntryPoint.Internals {
    internal static class ArgumentParserFactory {
        public static IOptionParser Option {
            get {
                return new OptionParser();
            }
        }
        public static IOptionParser OptionParameter {
            get {
                return new OptionParameterParser();
            }
        }
    }
}
