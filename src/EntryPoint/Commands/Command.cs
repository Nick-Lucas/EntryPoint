using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using System.Runtime.ExceptionServices;

namespace EntryPoint.Commands {
    internal class Command : BaseCommand {
        internal Command(BaseCliCommands parent, MethodInfo method) : base(parent, method) {
            Definition = method.GetCommandDefinition();
            Default = method.HasDefaultCommandAttribute();
        }

        // Command definition
        public CommandAttribute Definition { get; private set; }

        // Whether this Command is marked as default
        public bool Default { get; set; }


        // ** Execution **

        // Invoke the command.
        // MethodInfo.Invoke will wrap any thrown exceptions
        // so we unwrap and rethrow anything which bubbles up
        public void Execute(string[] args) {
            try {
                Method.Invoke(Parent, new object[] { args });
            } catch (TargetInvocationException e) {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            }
        }
    }
}
