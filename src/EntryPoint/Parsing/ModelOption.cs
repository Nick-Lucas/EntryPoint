using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;

namespace EntryPoint.Parsing {
    internal class ModelOption {
        internal ModelOption(PropertyInfo property) {
            Property = property;
            Option = property.GetOptionAttribute();
            Required = property.OptionRequired();
        }

        // The property from the ArgumentsModel
        public PropertyInfo Property { get; set; }
        
        // Whether the Option is required
        public bool Required { get; set; }

        // Option configuration
        public BaseOptionAttribute Option { get; set; }
    }
}
