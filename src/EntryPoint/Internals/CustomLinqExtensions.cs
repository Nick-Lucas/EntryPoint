using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {

    internal static class CustomLinqExtensions {
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
    }

}
