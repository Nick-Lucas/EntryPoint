using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EntryPoint.Common {

    internal static class TypeExtensions {
        public static bool IsList(this Type type) {
            return type.IsGenericOfType(typeof(List<>));
        }

        public static bool IsNullable(this Type type) {
            return type.IsGenericOfType(typeof(Nullable<>));
        }

        public static bool IsGenericOfType(this Type type, Type genericType) {
            return type.GetTypeInfo().IsGenericType
                && type.GetGenericTypeDefinition() == genericType;
        } 

        public static bool HasValue(this string s) {
            return s != null && s.Length > 0;
        }
    }

}
