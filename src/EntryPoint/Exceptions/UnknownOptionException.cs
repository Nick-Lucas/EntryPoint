using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class UnknownOptionException : UserFacingException {
        public UnknownOptionException(string message) : base(message) { }
    }
}
