using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;
using EntryPoint.OptionModel;

namespace EntryPoint.OptionStrategies {

    internal interface IOptionStrategy {
        object GetValue(ModelOption modelOption, TokenGroup tokenGroup);
        bool RequiresParameter { get; }
    }

}
