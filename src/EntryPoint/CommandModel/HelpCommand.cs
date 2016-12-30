using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace EntryPoint.CommandModel {
    public class HelpCommand : BaseCommand {
        public HelpCommand(BaseCommands baseCommands, MethodInfo method) 
            : base(baseCommands, method) { }

        public void Execute(CommandModel model) {
            string help = CommandHelpGenerator.Generate(model);
            try {
                Method.Invoke(Parent, new object[] { help });
            } catch (TargetInvocationException e) {
                throw e.InnerException;
            }
        }
    }
}
