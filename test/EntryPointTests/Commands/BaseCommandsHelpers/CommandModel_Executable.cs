using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.Commands.Helpers;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class CommandModel_Executable : BaseCliCommands {
        [Command("C1")]
        public void Command1(string[] args) {
            throw new CommandExecutedException("C1 " + string.Join(" ", args));
        }

        [DefaultCommand]
        [Command("C2")]
        public void Command2(string[] args) {
            throw new CommandExecutedException("C2 " + string.Join(" ", args));
        }

        public override void OnHelpInvoked(string commandsHelpText) {
            throw new CommandExecutedException("HELP");
        }
    }
}
