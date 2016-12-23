using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.OptionParsers {
    public interface IOptionParser {
        object GetValue(string[] args, Type outputType, BaseOptionAttribute definition);
    }
}
