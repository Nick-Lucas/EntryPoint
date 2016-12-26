using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using EntryPoint.Internals;
using EntryPoint.Parsing;
using EntryPoint.OptionModel;

namespace EntryPoint.OptionStrategies {

    internal class OptionParameterStrategy : IOptionStrategy {
        internal OptionParameterStrategy() { }

        // public object GetValue(List<Token> args, Type outputType, BaseOptionAttribute definition) {
        public object GetValue(ModelOption modelOption, TokenGroup tokenGroup) {
            object value = tokenGroup.Parameter.Value;
            return ValueConverter.ConvertValue(value, modelOption.Property.PropertyType);
        }

        public bool RequiresParameter {
            get {
                return true;
            }
        }
    }

}
