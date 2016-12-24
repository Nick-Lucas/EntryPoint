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

        //public object GetValue(List<Token> args, Type outputType, BaseOptionAttribute definition) {
        public object GetValue(ModelOption modelOption, TokenGroup tokenGroup) {
            object value = tokenGroup.ArgumentToken.Value;
            return ConvertValue(value, modelOption.Property.PropertyType);
        }

        //static string GetKnownValue(List<Token> args, int index) {
        //    if (args.Count - 1 == index) {
        //        throw new NoParameterException(
        //            $"The argument {args[index]} was the last argument, but a parameter for it was expected");
        //    }
        //    if (args[index + 1].IsOption) {
        //        throw new NoParameterException(
        //            $"The parameter for {args[index]} was another option");
        //    }
        //    return args[index + 1].Value;
        //}

        // TODO: do this process at the end of everything, once we know what we're missing from the tokens list
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
