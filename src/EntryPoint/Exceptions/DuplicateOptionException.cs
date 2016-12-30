using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class DuplicateOptionException : UserFacingException {
        public DuplicateOptionException(string message) : base(message) { }
    }
}
