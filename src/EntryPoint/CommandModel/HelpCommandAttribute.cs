using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.CommandModel {
    [AttributeUsage(
        AttributeTargets.Method, 
        AllowMultiple = false, 
        Inherited = true)]
    public class HelpCommandAttribute : Attribute {
    }
}
