using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.OptionParsers {
    internal interface IOptionParser {
        object GetValue(List<Token> args, Type outputType, BaseOptionAttribute definition);
    }
}
