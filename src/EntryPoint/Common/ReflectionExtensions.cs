using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using System.Reflection;
using EntryPoint.Help;

namespace EntryPoint.Common {
    internal static class ReflectionExtensions {

        // Get the HelpAttribute from a class or property
        internal static HelpAttribute GetHelp(this MemberInfo member) {
            return member.GetCustomAttribute<HelpAttribute>()
                ?? new HelpAttribute();
        }

        // Get the HelpAttribute from a class or property
        internal static HelpAttribute GetHelp(this MethodInfo member) {
            return member.GetCustomAttribute<HelpAttribute>()
                ?? new HelpAttribute();
        }

        // Get the base type without using reflection elsewhere
        internal static Type BaseType(this Type type) {
            return type.GetTypeInfo().BaseType;
        }

        internal static bool IsList(this Type type) {
            return type.GetTypeInfo().GetInterface("IList") != null;
        }
    }
}
