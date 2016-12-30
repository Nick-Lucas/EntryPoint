using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Common;
using System.Reflection;

namespace EntryPoint.Arguments.OptionStrategies {
    internal static class ValueConverter {

        // Sanitise values before trying to store them
        public static object ConvertValue(object value, Type outputType) {
            if (value == null) {
                return value;
            }

            if (Nullable.GetUnderlyingType(outputType) != null) {
                return Convert.ChangeType(value, Nullable.GetUnderlyingType(outputType));
            }
            value = SanitiseSpecialTypes(value, outputType);
            return Convert.ChangeType(value, outputType);
        }

        static object SanitiseSpecialTypes(object value, Type outputType) {
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
        static object SanitiseBool(object value) {
            int v;
            if (int.TryParse(value.ToString(), out v)) {
                value = (v != 0);
            }
            return value;
        }

        // Converts an int or string representation of an application enum into that enum
        static object SanitiseEnum(object value, Type outputType) {
            return Enum.Parse(outputType, value.ToString(), true);
        }
    }
}
