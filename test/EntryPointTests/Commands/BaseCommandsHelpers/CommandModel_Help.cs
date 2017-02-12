using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using EntryPointTests.Commands.Helpers;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class CommandModel_Help : BaseCliCommands {
        [Command("C1")]
        public void Command1(string[] args) {
            throw new CommandExecutedException("C1");
        }

        [DefaultCommand]
        [Command("C2")]
        public void Command2(string[] args) {
            throw new CommandExecutedException("C2");
        }

        public override void OnHelpInvoked(string commandsHelpText) {
            throw new CommandExecutedException("HELP");
        }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
