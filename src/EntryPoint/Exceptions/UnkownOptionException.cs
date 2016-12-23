using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class UnkownOptionException : ArgumentException {
        public UnkownOptionException(string message) : base(message) { }
    }
}
