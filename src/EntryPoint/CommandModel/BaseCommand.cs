using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EntryPoint.CommandModel {
    internal class BaseCommand {
        internal BaseCommand(BaseCommands parent, MethodInfo method) {
            Parent = parent;
            Method = method;
        }

        // Command class to invoke on
        protected BaseCommands Parent { get; set; }

        // Method definition to invoke
        public MethodInfo Method { get; private set; }
    }
}
