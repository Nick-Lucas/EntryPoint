using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// Thrown whenever a user invokes the same option twice in a command
    /// </summary>
    public class DuplicateOptionException : UserFacingException {
        internal DuplicateOptionException(string message) : base(message) { }
    }

}
