using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    internal class ParseResult {
        public List<TokenGroup> TokenGroups { get; set; } = new List<TokenGroup>();
        public List<Token> Operands { get; set; } = new List<Token>();
    }
}
