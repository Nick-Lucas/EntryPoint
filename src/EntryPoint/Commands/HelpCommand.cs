using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace EntryPoint.Commands {
    internal class HelpCommand : BaseCommand {
        internal HelpCommand(BaseCliCommands baseCommands, MethodInfo method) 
            : base(baseCommands, method) { }

        public void Execute(CommandModel model, string message = null) {
            string spacer = message == null ? "" : "\n\n";
            string help = CliCommandsHelp.Generate(model);
            string fullHelp = $"{message}{spacer}{help}";
            
            try {
                Method.Invoke(Parent, new object[] { fullHelp });
            } catch (TargetInvocationException e) {
                throw e.InnerException;
            }
        }
    }
}
