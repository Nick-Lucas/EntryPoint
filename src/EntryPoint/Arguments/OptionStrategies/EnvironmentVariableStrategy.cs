using System;
using System.Collections.Generic;
using System.Text;
using EntryPoint.Parsing;

namespace EntryPoint.Arguments.OptionStrategies {

    internal class EnvironmentVariableStrategy {
        public object GetValue(Type type, string variableName) {
            object value = Environment.GetEnvironmentVariable(variableName);
            return ValueConverter.ConvertValue(value, type);
        }
    }

}
