using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.OptionParsers {
    internal class OptionParser : IOptionParser {
        internal OptionParser() { }

        public object GetValue(List<Token> args, Type outputType, BaseOptionAttribute definition) {
            var value = HasDouble(args, definition) || HasSingle(args, definition.SingleDashChar);
            return CheckValue(value, outputType, definition);
        }

        bool HasSingle(List<Token> args, char? argName) {
            if (argName == null) {
                return false;
            }
            return args.SingleDashIndex(argName.Value) >= 0;
        }

        bool HasDouble(List<Token> args, BaseOptionAttribute definition) {
            return definition.DoubleDashIndex(args) >= 0;
        }

        object CheckValue(bool value, Type outputType, BaseOptionAttribute definition) {
            if (outputType != typeof(bool)) {
                throw new InvalidOperationException(
                    $"The type of {EntryPointApi.DASH_DOUBLE}{definition.DoubleDashName} on the ArgumentsModel, " 
                    + $"must be a boolean for {nameof(OptionAttribute)}");
            }
            return value;
        }
    }
}
