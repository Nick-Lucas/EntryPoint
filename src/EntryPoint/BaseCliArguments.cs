﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Interfaces;
using EntryPoint.Helpers;

namespace EntryPoint {

    /// <summary>
    /// The base class which must be derived from for a CliArguments implementation
    /// </summary>
    public abstract class BaseCliArguments : ICliHelpable {
        /// <summary>
        /// The base class which must be derived from for  CliArguments implementation
        /// </summary>
        /// <param name="utilityName">The name of your utility or application</param>
        public BaseCliArguments(string utilityName) {
            UtilityName = utilityName;
        }
        internal BaseCliArguments() { }

        internal string UtilityName { get; set; }

        /// <summary>
        /// All trailing arguments left after any Options and OptionParameters defined in CliArguments
        /// </summary>
        public string[] Operands { get; internal set; }


        // ** Baked in Functionality **
        
        [Option(
            LongName = "help", ShortName = 'h')]
        [Help(
            "Display the Help Documentation")]
        public bool HelpInvoked { get; set; }

        /// <summary>
        /// Invoked when the user invokes -h/--help with no explicit command
        /// </summary>
        /// <param name="args">Any remaining arguments after --help</param>
        [HelpInvoker]
        public abstract void OnHelpInvoked(string helpText);
    }

}
