using System;
using System.Collections.Generic;
using System.Text;
using EntryPoint.Parsing;
using EntryPoint.Exceptions;

namespace EntryPoint.Arguments.OptionStrategies {

    internal class EnvironmentVariableStrategy {
        public object GetValue(EnvironmentVariable envVar) {
            Type type = envVar.Property.PropertyType;
            string variableName = envVar.Definition.VariableName;

            object value = Environment.GetEnvironmentVariable(variableName);
            if (value == null && envVar.Required) {
                throw new RequiredException(
                    $"The environment variable `{variableName}` " +
                    $"was not provided, but is required");
            }

            return ValueConverter.ConvertValue(value, type);
        }
    }

}
