using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {

    internal static class TypeExtensions {
        public static bool CanBeNull(this Type type) {
            if (type.IsNullable()) {
                return true;
            }
            try { // TODO: fix this dirty hack
                Activator.CreateInstance(type);
            } catch (Exception) {
                return true;
            }
            return false;
        }

        public static bool IsNullable(this Type type) {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }

}
