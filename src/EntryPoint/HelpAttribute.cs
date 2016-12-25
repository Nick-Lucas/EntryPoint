using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {

    /// <summary>
    /// Used to provide additional information to display in the --help Option. May be applied to:
    /// Class: for a description of the utility
    /// Property: for a description of the option's usage
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Property,
        AllowMultiple = true,
        Inherited = true)]
    public class HelpAttribute : Attribute {

        /// <summary>
        /// Additional information to display when --help is invoked
        /// </summary>
        /// <param name="detail">A description of the utility/option's usage</param>
        public HelpAttribute(string detail) {
            _detail = detail;
        }
        internal HelpAttribute() { }

        /// <summary>
        /// A description of the utility/option's usage
        /// </summary>
        public string Detail {
            get {
                return _detail.Trim();
            }
        }
        string _detail = "";
    }

}
