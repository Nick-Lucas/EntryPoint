using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// The base exception used for all exceptions caused by the user: incorrect syntax or other argument mistakes
    /// </summary>
    public abstract class UserFacingException : ArgumentException {
        internal UserFacingException(string message) : base(message) { }
    }

}
