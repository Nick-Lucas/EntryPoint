using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class NoParameterException : UserFacingException {
        public NoParameterException(string message) : base(message) { }
    }
}
