using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    internal struct Token : IEquatable<Token> {
        public string Value { get; set; }
        public bool IsOption { get; set; }

        internal Token(string text, bool isOption) {
            Value = text;
            IsOption = isOption;
        }

        public bool Equals(Token other) {
            return this.Value == other.Value 
                && this.IsOption == other.IsOption;
        }
    }
}
