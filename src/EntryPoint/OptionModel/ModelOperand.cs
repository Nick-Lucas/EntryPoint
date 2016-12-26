using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Internals;
using EntryPoint.OptionStrategies;
using System.Reflection;

namespace EntryPoint.OptionModel {
    internal class ModelOperand {
        public ModelOperand(PropertyInfo property) {
            Property = property;
            Definition = property.GetOperandDefinition();
        }

        public PropertyInfo Property { get; set; }
        public OperandAttribute Definition { get; set; }
        public OperandStrategy OperandStrategy { get; private set; } = new OperandStrategy();
    }
}
