using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;

namespace EntryPoint.CommandModel {
    public class Command {
        private BaseCommands Parent { get; set; }
        public MethodInfo Method { get; private set; }

        public CommandAttribute Definition { get; private set; }
        public bool Default { get; set; }

        public Command(BaseCommands parent, MethodInfo method) {
            Parent = parent;
            Method = method;
            Definition = method.GetCustomAttribute<CommandAttribute>();
            Default = Method.GetCustomAttribute<DefaultCommandAttribute>() != null;
        }


        // ** Execution **

        public void Execute(string[] args) {
            try {
                Method.Invoke(Parent, new object[] { args });
            } catch (TargetInvocationException e) {
                throw e.InnerException;
            }
        }
    }
}
