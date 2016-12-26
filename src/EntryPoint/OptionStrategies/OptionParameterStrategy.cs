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
            switch (definition.ParameterDefaultBehaviour) {
                case ParameterDefaultEnum.DefaultValue:
                    if (modelOption.Property.PropertyType.CanBeNull()) {
                        return null;
                    }
                    return Activator.CreateInstance(modelOption.Property.PropertyType);

                case ParameterDefaultEnum.CustomValue:
                    return definition.ParameterDefaultValue;

                default:
                    throw new NotSupportedException(
                        $"Unsupported {nameof(ParameterDefaultEnum)} state: {definition.ParameterDefaultBehaviour}");
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
            value = SanitiseSpecialTypes(value, outputType);
            return Convert.ChangeType(value, outputType);
        }

        object SanitiseSpecialTypes(object value, Type outputType) {
            if (outputType == typeof(bool)) {
                return SanitiseBool(value);
            }
            if (outputType.BaseType() == typeof(Enum) || outputType == typeof(Enum)) {
                return SanitiseEnum(value, outputType);
            }
            return value;
        }

        // Converts an int or string representation of a bool into a bool
        // todo: what about bool.TryParse(...)? probably more appropriate as it supports string representations natively
        object SanitiseBool(object value) {
            int v;
            if (int.TryParse(value.ToString(), out v)) {
                value = (v != 0);
            }
            return value;
        }

        // Converts an int or string representation of an application enum into that enum
        object SanitiseEnum(object value, Type outputType) {
            return Enum.Parse(outputType, value.ToString());
        }
    }

}
