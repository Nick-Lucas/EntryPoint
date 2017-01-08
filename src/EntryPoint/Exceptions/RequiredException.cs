using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// Thrown whenever a user does not provide an argument which is marked 'Required'
    /// </summary>
    public class RequiredException : UserFacingException {
        internal RequiredException(string message) : base(message) { }
    }

}
