using System;
using System.Collections.Generic;
using System.Text;
using EntryPoint.Parsing;
using EntryPoint.Exceptions;

namespace EntryPoint.Arguments.OptionStrategies {

    internal class EnvironmentVariableStrategy {
        public object GetValue(Type type, string variableName, bool required) {
            object value = Environment.GetEnvironmentVariable(variableName);
            if (value == null && required) {
                throw new RequiredException(
                    $"The environment variable `{variableName}` " +
                    $"was not provided, but is required");
            }
            return ValueConverter.ConvertValue(value, type);
        }
    }

}
