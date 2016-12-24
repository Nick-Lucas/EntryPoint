using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public class InvalidModelException : Exception {
        internal InvalidModelException(string message) : base(message) { }
    }
}
