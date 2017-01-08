using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// Thrown whenever there is a problem with the provided CliArguments/CliCommands implementation
    /// </summary>
    public class InvalidModelException : Exception {
        internal InvalidModelException(string message) : base(message) { }
    }

}
