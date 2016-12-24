using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    internal class BaseOptionAttributeEqualityComparer : IEqualityComparer<BaseOptionAttribute> {
        public bool Equals(BaseOptionAttribute x, BaseOptionAttribute y) {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(BaseOptionAttribute obj) {
            return 1;
        }
    }
}
