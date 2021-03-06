﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {

    /// <summary>
    /// Used to provide additional information to display in the --help Option. May be applied to:
    /// Class: for a description of the utility
    /// Property: for a description of the option's usage
    /// Method: for a Command class's command methods
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class HelpAttribute : Attribute {

        /// <summary>
        /// Additional information to display when --help is invoked
        /// </summary>
        /// <param name="Detail">A description of the utility/option's usage</param>
        public HelpAttribute(string Detail) {
            this.Detail = Detail.Trim();
        }
        internal HelpAttribute() : this("") { }

        /// <summary>
        /// A description of the utility/option's usage
        /// </summary>
        public string Detail { get; set; }
        
    }

}
