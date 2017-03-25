using System;
using System.Collections.Generic;
using System.Text;

namespace EntryPoint.Exceptions {

    /// <summary>
    /// Thrown when the user provides a value which cannot be converted or cast to the output type
    /// </summary>
    public class VariableTypeException : UserFacingException {
        internal VariableTypeException(string message) : base(message) { }
    }

}
