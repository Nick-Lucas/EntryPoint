using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    internal class TokenEqualityComparer : IEqualityComparer<Token> {
        public bool Equals(Token x, Token y) {
            return x.Equals(y);
        }

        public int GetHashCode(Token obj) {
            return 1;
        }
    }
}
