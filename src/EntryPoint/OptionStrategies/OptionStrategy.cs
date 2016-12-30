using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;
using EntryPoint.ArgumentModel;

namespace EntryPoint.OptionStrategies {

    internal class OptionStrategy : IOptionStrategy {
        internal OptionStrategy() { }

        public object GetValue(ModelOption modelOption, TokenGroup tokenGroup) {
            var value = HasDoubleOption(tokenGroup.Option, modelOption.Definition) 
                     || HasSingleOption(tokenGroup.Option, modelOption.Definition.ShortName);
            return CheckValue(value, modelOption.Property.PropertyType, modelOption.Definition);
        }

        public bool RequiresParameter {
            get {
                return false;
            }
        }


        // ** Helpers **

        bool HasSingleOption(Token arg, char? argName) {
            if (argName == null) {
                return false;
            }
            return arg.IsSingleDashOption();
        }

        bool HasDoubleOption(Token arg, BaseOptionAttribute definition) {
            return arg.IsDoubleDashOption();
        }

        object CheckValue(bool value, Type outputType, BaseOptionAttribute definition) {
            if (outputType != typeof(bool)) {
                throw new InvalidOperationException(
                    $"The type of {Cli.DASH_DOUBLE}{definition.LongName} on the ArgumentsModel, " 
                    + $"must be a boolean for {nameof(OptionAttribute)}");
            }
            return value;
        }
    }

}
