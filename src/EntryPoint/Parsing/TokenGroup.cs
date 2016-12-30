using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {

    internal class TokenGroup {
        public Token Option { get; set; }

        public bool TakesParameter { get; set; }
        public Token Parameter { get; set; }
    }

}
