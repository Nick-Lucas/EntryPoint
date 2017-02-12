using EntryPoint.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Commands {
    internal static class CommandFacade {
        public static C Execute<C>(C commands, string[] args) where C : BaseCliCommands {
            try {
                ExecuteCommand(commands, args);
            } catch (UserFacingException e) {
                commands.UserFacingExceptionThrown = true;
                commands.OnUserFacingException(e, e.Message);
            }
            return commands;
        }

        private static void ExecuteCommand<C>(C commands, string[] args) where C : BaseCliCommands {
            var model = new CommandModel(commands);
            model.Execute(args);
        }
    }
}
