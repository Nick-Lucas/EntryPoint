using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPointTests.Commands {
    public class CommandExecutedException : ArgumentException {
        public CommandExecutedException(string param) : base("", param) { }
    }
}
