using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {

    /// <summary>
    /// The base class which must be derived from for an OptionsModel  implementation
    /// </summary>
    public abstract class BaseApplicationOptions {
        /// <summary>
        /// The base class which must be derived from for an OptionsModel implementation
        /// </summary>
        /// <param name="utilityName">The name of your utility or application</param>
        public BaseApplicationOptions(string utilityName) {
            UtilityName = utilityName;
        }

        internal string UtilityName { get; set; }
    
        /// <summary>
        /// All trailing arguments left after the list of Options and OptionParameters
        /// </summary>
        public string[] Operands { get; internal set; }
    }

}
