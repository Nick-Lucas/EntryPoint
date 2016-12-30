using EntryPoint;
using EntryPointTests.Commands.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class CommandModel_DuplicateNames : BaseCliCommands {
        [Command("C1")]
        public void Command1(string[] args) {
            throw new CommandExecutedException("C1");
        }

        [DefaultCommand]
        [Command("C1")]
        public void Command2(string[] args) {
            throw new CommandExecutedException("C2");
        }

        public override void Help(string commandsHelpText) {
            throw new NotImplementedException();
        }
    }
}
