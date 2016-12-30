﻿using EntryPoint.Arguments.OptionStrategies;
using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Arguments {

    /// <summary>
    /// The Base class for all argument attributes
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property, 
        AllowMultiple = true, 
        Inherited = true)]
    public class BaseOptionAttribute : Attribute {
        internal BaseOptionAttribute(IOptionStrategy optionStrategy) {
            OptionStrategy = optionStrategy;
        }
        internal IOptionStrategy OptionStrategy { get; private set; }

        /// <summary>
        /// The case sensitive character which can be declared after a - to trigger an option 
        /// </summary>
        public char ShortName { get; set; }

        /// <summary>
        /// The case insensitive string which can be declared after a -- to trigger an option
        /// </summary>
        public string LongName { get; set; }
    }

}