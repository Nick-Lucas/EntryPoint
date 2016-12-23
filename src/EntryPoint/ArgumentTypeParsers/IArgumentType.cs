using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.ArgumentTypeParsers {
    public interface IArgumentType {
        string GetValue(string[] args, BaseArgumentAttribute definition);
    }
}
