using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace EntryPoint.CommandModel {
    public class Command {
        // Command class to invoke on
        private BaseCommands Parent { get; set; }

        // Method definition to invoke
        public MethodInfo Method { get; private set; }

        // Command definition
        public CommandAttribute Definition { get; private set; }

        // Whether this Command is marked as default
        public bool Default { get; set; }

        public Command(BaseCommands parent, MethodInfo method) {
            Parent = parent;
            Method = method;
            Definition = method.GetCommandDefinition();
            Default = method.HasDefaultCommandAttribute();
        }


        // ** Execution **

        // Invoke the command.
        // MethodInfo.Invoke will wrap any thrown exceptions
        // so we unwrap and rethrow anything which bubbles up
        public void Execute(string[] args) {
            try {
                Method.Invoke(Parent, new object[] { args });
            } catch (TargetInvocationException e) {
                throw e.InnerException;
            }
        }
    }
}
