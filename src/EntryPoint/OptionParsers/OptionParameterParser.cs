using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.OptionParsers {
    internal class OptionParameterParser : IOptionParser {
        internal OptionParameterParser() { }

        // public object GetValue(List<Token> args, Type outputType, BaseOptionAttribute definition) {
        public object GetValue(ModelOption modelOption, TokenGroup tokenGroup) {
            object value = tokenGroup.ArgumentToken.Value;
            return ConvertValue(value, modelOption.Property.PropertyType);
        }

        // Get the default value for the Option's definition
        public object GetDefaultValue(ModelOption modelOption) {
            var value = CalculateDefaultValue(modelOption);
            var type = modelOption.Property.PropertyType;
            return ConvertValue(value, type);
        }
        object CalculateDefaultValue(ModelOption modelOption) {
            var definition = (OptionParameterAttribute)modelOption.Definition;
            switch (definition.NullValueBehaviour) {
                case ParameterDefaultEnum.DefaultValue:
                    if (modelOption.Property.PropertyType.CanBeNull()) {
                        return null;
                    }
                    return Activator.CreateInstance(modelOption.Property.PropertyType);

                case ParameterDefaultEnum.CustomValue:
                    return definition.CustomDefaultValue;

                default:
                    throw new NotSupportedException(
                        $"Unsupported {nameof(ParameterDefaultEnum)} state: {definition.NullValueBehaviour}");
            }
        }

        public bool RequiresParameter {
            get {
                return true;
            }
        }

        // Sanitise values before trying to store them
        public object ConvertValue(object value, Type outputType) {
            if (value == null) {
                return value;
            }

            if (Nullable.GetUnderlyingType(outputType) != null) {
                return Convert.ChangeType(value, Nullable.GetUnderlyingType(outputType));
            }
            value = SupportIntBools(value, outputType);
            return Convert.ChangeType(value, outputType);
        }

        object SupportIntBools(object value, Type outputType) {
            if (outputType == typeof(bool)) {
                int v;
                if (int.TryParse(value.ToString(), out v)) {
                    value = (v != 0);
                }
            }
            return value;
        }
    }
}
