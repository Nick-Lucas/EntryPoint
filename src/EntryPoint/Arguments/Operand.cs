using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Common;
using EntryPoint.Arguments.OptionStrategies;
using System.Reflection;

namespace EntryPoint.Arguments {
    internal class Operand {

        public Operand(PropertyInfo property) {
            Property = property;
            Definition = property.GetOperandDefinition();
            Required = property.HasRequiredAttribute();
            Help = property.GetHelp();
        }

        // The original property on the ApplicationOptions implementation
        public PropertyInfo Property { get; set; }

        // Operand attribute
        public OperandAttribute Definition { get; set; }

        // Strategy for value getting
        public OperandStrategy OperandStrategy { get; private set; } = new OperandStrategy();

        // Whether the Option is required
        public bool Required { get; private set; }

        // Help attribute
        public HelpAttribute Help { get; internal set; }
    }

}
