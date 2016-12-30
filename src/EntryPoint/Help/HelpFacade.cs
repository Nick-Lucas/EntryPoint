using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Commands;

namespace EntryPoint.Help {
    internal class HelpFacade {
        internal HelpFacade(BaseHelpable helpable) {
            Helpable = helpable;
        }

        // Command class to invoke on
        protected BaseHelpable Helpable { get; set; }

        public void Execute(string message = null) {
            string spacer = message == null ? "" : "\n\n";
            string help = Cli.GetHelp(Helpable);
            string fullHelp = $"{message}{spacer}{help}";

            Helpable.HelpInvoked = true;
            Helpable.OnHelpInvoked(fullHelp);
        }
    }
}
