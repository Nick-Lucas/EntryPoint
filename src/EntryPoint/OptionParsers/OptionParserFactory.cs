using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.OptionParsers {
    internal static class OptionParserFactory {
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
