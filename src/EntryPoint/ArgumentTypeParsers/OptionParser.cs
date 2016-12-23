using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.ArgumentTypeParsers {
    public class OptionParser : IOptionParser {
        internal OptionParser() { }

        public string GetValue(string[] args, BaseArgumentAttribute definition) {
            return (HasDouble(args, definition)
                || HasSingle(args, definition.SingleDashChar)).ToString();
        }

        bool HasSingle(string[] args, char? argName) {
            if (argName == null) {
                return false;
            }
            return args.SingleDashIndex(argName.Value) >= 0;
        }

        bool HasDouble(string[] args, BaseArgumentAttribute definition) {
            return definition.DoubleDashIndex(args) >= 0;
        }
    }
}
