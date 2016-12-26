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

        // Get the default value for the Option's definition
        public object GetDefaultValue(ModelOption modelOption) {
            var parameterDefinition = (OptionParameterAttribute)modelOption.Definition;
            var type = modelOption.Property.PropertyType;
            var value = ValueConverter.CalculateDefaultValue(parameterDefinition, type);
            return ValueConverter.ConvertValue(value, type);
        }

        public bool RequiresParameter {
            get {
                return true;
            }
        }
    }

}
