using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using EntryPoint.Helpers;
using EntryPoint.Parsing;
using EntryPoint.Arguments;

namespace EntryPoint.Arguments.OptionStrategies {

    internal class OptionParameterStrategy : IOptionStrategy {
        internal OptionParameterStrategy() { }

        // public object GetValue(List<Token> args, Type outputType, BaseOptionAttribute definition) {
        public object GetValue(Option modelOption, TokenGroup tokenGroup) {
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
