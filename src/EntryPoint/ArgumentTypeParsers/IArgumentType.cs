using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Helpers;

namespace EntryPoint.ArgumentTypeParsers {
    public interface IArgumentType {
        string GetValue(string[] args, BaseArgumentAttribute definition);
    }
}
