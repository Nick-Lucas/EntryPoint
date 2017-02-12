using EntryPoint;
using EntryPointTests.Commands.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryPoint.Exceptions;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class CommandModel_MethodSig_NoArgs : BaseCliCommands {
        [Command("C1")]
        public void Command1() {
            throw new CommandExecutedException("C1");
        }

        public override void OnHelpInvoked(string commandsHelpText) {
            throw new NotImplementedException();
        }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
