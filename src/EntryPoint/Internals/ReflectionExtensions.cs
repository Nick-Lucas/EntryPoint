using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using System.Reflection;

namespace EntryPoint.Internals {
    internal static class ReflectionExtensions {
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

        // Get the HelpAttribute from a class or property
        internal static HelpAttribute GetHelp(this MemberInfo member) {
            return member.GetCustomAttribute<HelpAttribute>()
                ?? new HelpAttribute();
        }

        // Get the base type without using reflection elsewhere
        internal static Type BaseType(this Type type) {
            return type.GetTypeInfo().BaseType;
        }
    }
}
