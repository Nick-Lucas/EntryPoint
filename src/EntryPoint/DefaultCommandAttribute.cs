using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class DefaultCommandAttribute : Attribute {

        /// <summary>
        /// Marks a Command as the Default fallback, when no known Command is given
        /// </summary>
        public DefaultCommandAttribute() { }

    }
}
