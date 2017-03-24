using System;
using System.Collections.Generic;
using System.Text;

using EntryPoint.Common;
using EntryPoint.Arguments.OptionStrategies;
using System.Reflection;

namespace EntryPoint.Arguments {
    internal class EnvironmentVariable {

        public EnvironmentVariable(PropertyInfo property) {
            Property = property;
            Definition = property.GetEnvironmentVariableDefinition();
            Required = property.HasRequiredAttribute();
            Help = property.GetHelp();
        }

        // The original property on the ApplicationOptions implementation
        public PropertyInfo Property { get; set; }

        // Operand attribute
        public EnvironmentVariableAttribute Definition { get; set; }

        // Strategy for value getting
        public EnvironmentVariableStrategy Strategy { get; private set; } = new EnvironmentVariableStrategy();

        // Whether the Variable is required
        public bool Required { get; private set; }

        // Help attribute
        public HelpAttribute Help { get; internal set; }

    }
}
