using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.OptionParsers {

    internal interface IOptionStrategy {
        object GetValue(ModelOption modelOption, TokenGroup tokenGroup);
        object GetDefaultValue(ModelOption modelOption);
        bool RequiresParameter { get; }
    }

}
