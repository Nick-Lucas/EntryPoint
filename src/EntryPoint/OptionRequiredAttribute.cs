using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {

    /// <summary>
    /// Used to mark an Option as required
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionRequiredAttribute : Attribute {

    }

}
