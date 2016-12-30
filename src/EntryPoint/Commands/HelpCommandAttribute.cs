using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Commands {
    [AttributeUsage(
        AttributeTargets.Method, 
        AllowMultiple = false, 
        Inherited = true)]
    internal class HelpCommandAttribute : Attribute {

    }
}
