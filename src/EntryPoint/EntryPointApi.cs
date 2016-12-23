using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint {
    public static class EntryPointApi {
        internal const string DASH_SINGLE = "-";
        internal const string DASH_DOUBLE = "--";

        public static A Parse<A>(string[] args) where A : Arguments, new() {
            return Parse(new A(), args);
        }
        public static A Parse<A>(A argumentsClass, string[] args) where A : Arguments {
            return Parser.ParseAttributes(argumentsClass, args);
        }
    }
}
