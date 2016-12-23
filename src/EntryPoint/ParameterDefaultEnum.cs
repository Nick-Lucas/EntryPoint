using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {
    public enum ParameterDefaultEnum {
        
        /// <summary>
        /// Causes option parameters which are not found to default to their type's default value
        /// </summary>
        DefaultValue,

        /// <summary>
        /// Allows you to set a custom default value, for when an option parameter is not found
        /// </summary>
        CustomValue

    }
}
