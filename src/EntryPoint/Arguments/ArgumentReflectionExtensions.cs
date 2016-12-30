using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using System.Reflection;
using EntryPoint.Helpers;

namespace EntryPoint.Arguments {
    public static class ArgumentReflectionExtensions {

        internal static BaseOptionAttribute GetOptionDefinition(this PropertyInfo prop) {
            var attributes = prop.GetCustomAttributes<BaseOptionAttribute>().ToList();
            if (attributes.Count > 1) {
                throw new InvalidModelException(
                    $"More than one Option attribute was applied to {prop.Name}");
            }
            if (!attributes.Any()) {
                return null;
            }
            return attributes.First();
        }

        internal static OperandAttribute GetOperandDefinition(this PropertyInfo prop) {
            return prop.GetCustomAttribute<OperandAttribute>();
        }

        internal static bool HasRequiredAttribute(this PropertyInfo prop) {
            return prop.GetCustomAttribute<RequiredAttribute>() != null;
        }
    }

}
