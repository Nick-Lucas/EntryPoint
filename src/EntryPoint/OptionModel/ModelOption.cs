using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Internals;

namespace EntryPoint.OptionModel {

    internal class ModelOption {
        internal ModelOption(PropertyInfo property) {
            Property = property;
            Definition = property.GetOptionDefinition();
            Required = property.HasRequiredAttribute();
            Help = property.GetHelp();
        }

        // The property from the ArgumentsModel
        public PropertyInfo Property { get; private set; }
        
        // Whether the Option is required
        public bool Required { get; private set; }

        // Option configuration
        public BaseOptionAttribute Definition { get; private set; }

        // Provided documentation
        public HelpAttribute Help { get; private set; }
    }

}
