using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Commands {
    public class CommandModel_NoDefaults : BaseCommands {
        [Command("C1")]
        public void Command1(string[] args) {
            throw new CommandExecutedException("C1");
        }

        [Command("C2")]
        public void Command2(string[] args) {
            throw new CommandExecutedException("C2");
        }
    }
}
