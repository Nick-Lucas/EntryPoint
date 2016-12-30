using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class UserFacingException : ArgumentException {
        public UserFacingException(string message) : base(message) { }
    }
}
