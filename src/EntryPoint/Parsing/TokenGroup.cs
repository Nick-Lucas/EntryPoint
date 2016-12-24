using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    internal class TokenGroup {
        public Token OptionToken { get; set; }

        public bool RequiresArgument { get; set; }
        public Token ArgumentToken { get; set; }
    }
}
