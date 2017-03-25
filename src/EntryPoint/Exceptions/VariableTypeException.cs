using System;
using System.Collections.Generic;
using System.Text;

namespace EntryPoint.Exceptions {
    public class VariableTypeException : UserFacingException {
        internal VariableTypeException(string message) : base(message) { }
    }
}
