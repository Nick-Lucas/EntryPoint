using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Common {

    internal static class CustomExtensions {
        internal static List<T> Duplicates<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null) {
            HashSet<T> hash = new HashSet<T>(comparer);
            List<T> result = new List<T>();

            foreach (var item in items) {
                if (hash.Contains(item)) {
                    result.Add(item);
                } else {
                    hash.Add(item);
                }
            }

            return result;
        }

        internal static T IfTrue<T>(this bool b, T show) {
            return b ? show : default(T);
        }
    }

}
