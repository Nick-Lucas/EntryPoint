using EntryPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryPoint.Exceptions;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class CommandModel_RequiredOptions : BaseCliCommands {
        [Command("Main")]
        public void Main(string[] args) {
            var a = Cli.Parse<ArgumentModel_RequiredOptions>(args);
            throw new Helpers.CommandExecutedException(a.MyOption.ToString());
        }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
