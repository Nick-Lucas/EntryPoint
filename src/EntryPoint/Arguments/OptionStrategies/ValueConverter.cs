﻿using System;
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
            if (value is IConvertible) {
                return Convert.ChangeType(value, outputType);
            } else if (value.GetType() == outputType) {
                return value;
            }
            throw new InvalidCastException(
                $"The requested type `{outputType.Name}` could not be converted to "
                + $"from the type: `{value.GetType().Name}` with value: `{value.ToString()}`");
        }

        static object SanitiseSpecialTypes(object value, Type outputType) {
            if (outputType == typeof(bool)) {
                return SanitiseBool(value);
            }
            if (outputType.BaseType() == typeof(Enum) || outputType == typeof(Enum)) {
                return SanitiseEnum(value, outputType);
            }
            if (outputType.IsList()) {
                return ProcessList(value, outputType);
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

        // Split a serialised list by its delimiters 
        // and convert its values to its core generic type
        // TODO: This might not be the best approach, but does work
        static object ProcessList(object serialisedList, Type listType) {
            var typeArg = listType.GenericTypeArguments[0];
            var reflectedList = Activator.CreateInstance(listType);
            var listAddMethod = reflectedList.GetType().GetRuntimeMethod("Add", new Type[] { typeArg });
            var stringValues = serialisedList.ToString().Split(',');
            foreach (var stringValue in stringValues) {
                object value = ConvertValue(stringValue, typeArg);
                listAddMethod.Invoke(reflectedList, new[] { value });
            }
            return reflectedList;
        }
    }
}
