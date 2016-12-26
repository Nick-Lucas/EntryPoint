using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class RequiredException : ArgumentException {
        public RequiredException(string message) : base(message) { }
    }
}
