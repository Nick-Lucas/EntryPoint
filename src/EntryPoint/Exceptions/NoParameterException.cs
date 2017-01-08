using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// Thrown whenever an OptionParameter is invoked, but no parameter argument is provided
    /// </summary>
    public class NoParameterException : UserFacingException {
        internal NoParameterException(string message) : base(message) { }
    }

}
