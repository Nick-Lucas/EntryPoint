using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {

    /// <summary>
    /// The base class which must be derived from for an ArgumentsModel implementation
    /// </summary>
    public class BaseApplicationOptions {
    
        /// <summary>
        /// All trailing arguments left after the list of Options and OptionParameters
        /// </summary>
        public string[] Operands { get; internal set; }

    }

}
