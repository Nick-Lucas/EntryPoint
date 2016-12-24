using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.OptionParsers {
    internal interface IOptionParser {
        object GetValue(ModelOption modelOption, TokenGroup tokenGroup);
    }
}
