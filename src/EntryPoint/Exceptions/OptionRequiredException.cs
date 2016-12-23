using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class OptionRequiredException : ArgumentException {
        public OptionRequiredException(string message) : base(message) { }
    }
}
