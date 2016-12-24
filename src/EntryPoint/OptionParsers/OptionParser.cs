using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.OptionParsers {
    internal class OptionParser : IOptionParser {
        internal OptionParser() { }

        public object GetValue(ModelOption modelOption, TokenGroup tokenGroup) {
            var value = HasDouble(tokenGroup.OptionToken, modelOption.Definition) 
                     || HasSingle(tokenGroup.OptionToken, modelOption.Definition.SingleDashChar);
            return CheckValue(value, modelOption.Property.PropertyType, modelOption.Definition);
        }

        bool HasSingle(Token arg, char? argName) {
            if (argName == null) {
                return false;
            }
            return arg.IsSingleDashOption();
        }

        bool HasDouble(Token arg, BaseOptionAttribute definition) {
            return arg.IsDoubleDashOption();
        }

        object CheckValue(bool value, Type outputType, BaseOptionAttribute definition) {
            if (outputType != typeof(bool)) {
                throw new InvalidOperationException(
                    $"The type of {EntryPointApi.DASH_DOUBLE}{definition.DoubleDashName} on the ArgumentsModel, " 
                    + $"must be a boolean for {nameof(OptionAttribute)}");
            }
            return value;
        }

        public object GetDefaultValue(ModelOption modelOption) {
            return false;
        }
    }
}
