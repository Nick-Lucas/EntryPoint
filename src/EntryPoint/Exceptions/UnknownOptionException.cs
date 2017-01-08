using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// Thrown whenever the user invoked an option which is not recognised
    /// </summary>
    public class UnknownOptionException : UserFacingException {
        internal UnknownOptionException(string message) : base(message) { }
    }

}
