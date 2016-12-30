using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.CommandModel;

namespace EntryPoint {
    public abstract class BaseCommands {
        /// <summary>
        /// Invoked when the user invokes -h/--help with no explicit command
        /// </summary>
        /// <param name="args">Any remaining arguments after --help</param>
        [HelpCommand]
        public abstract void Help(string commandsHelpText);
    }
}
