using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using EntryPoint.Internals;

namespace EntryPoint.OptionParsers {
    public class OptionParameterParser : IOptionParser {
        internal OptionParameterParser() { }

        public object GetValue(string[] args, Type outputType, BaseOptionAttribute definition) {
            int index = -1;
            object value = null;

            index = definition.SingleDashIndex(args);
            if (index >= 0) {
                value = GetKnownValue(args, index);
                
            } else { 
                index = definition.DoubleDashIndex(args);
                if (index >= 0) {
                    value = GetKnownValue(args, index);
                } else {
                    value = HandleMissingValue(outputType, definition);
                }
            }

            return ConvertValue(value, outputType);
        }

        static string GetKnownValue(string[] args, int index) {
            if (args[index].Contains("=")) {
                return args[index]
                    .Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                    .Last();

            } else {
                if (args.Length - 1 == index) {
                    throw new NoParameterException(
                        $"The argument {args[index]} was the last argument, but a parameter for it was expected");
                }
                if (args[index + 1].StartsWith(EntryPointApi.DASH_SINGLE)) {
                    throw new NoParameterException(
                        $"The parameter for {args[index]} was another option");
                }
                return args[index + 1];
            }
        }

        public object HandleMissingValue(Type outputType, BaseOptionAttribute argDefinition) {
            var definition = (OptionParameterAttribute)argDefinition;
            switch (definition.NullValueBehaviour) {
                case ParameterDefaultEnum.DefaultValue:
                    if (outputType.CanBeNull()) {
                        return null;
                    }
                    return Activator.CreateInstance(outputType);

                case ParameterDefaultEnum.CustomValue:
                    return definition.CustomDefaultValue;

                default:
                    throw new NotSupportedException(
                        $"Unsupported {nameof(ParameterDefaultEnum)} state: {definition.NullValueBehaviour}");
            }
        }

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
