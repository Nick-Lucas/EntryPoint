using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntryPoint.Parsing;
using EntryPoint.Exceptions;

namespace EntryPoint.Arguments.OptionStrategies {
    internal class EnvironmentVariableStrategy {

        public object GetValue(EnvironmentVariable envVar) {
            Type type = envVar.Property.PropertyType;
            string variableName = envVar.Definition.VariableName;

            object value = GetVariableOfName(variableName);
            if (value == null && envVar.Required) {
                throw new RequiredException(
                    $"The environment variable `{variableName}` " +
                    $"was not provided, but is required");
            }
            if (value == null) {
                return null;
            }

            return ValueConverter.ConvertValue(value, type);
        }

        Dictionary<string, string> EnvironmentVariables {
            get {
                if (_environmentVariables == null) {
                    var variables = Environment.GetEnvironmentVariables();
                    _environmentVariables = new Dictionary<string, string>(
                        StringComparer.CurrentCultureIgnoreCase);
                    foreach (var key in variables.Keys) {
                        EnvironmentVariables.Add(key.ToString(), variables[key].ToString());
                    }
                }
                return _environmentVariables;
            }
        }
        Dictionary<string, string> _environmentVariables = null;

        string GetVariableOfName(string variableName) {
            if (EnvironmentVariables.ContainsKey(variableName)) {
                return EnvironmentVariables[variableName];
            }
            return null;
        }

    }
}
