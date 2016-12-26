using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {
    public class OperandAttribute : Attribute {
        public OperandAttribute(int position) {
            Position = position;
        }

        /// <summary>
        /// The 1 Based position of the operand
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Selects what the default value behaviour for when the parameter is not provided
        /// </summary>
        public DefaultValueBehaviourEnum DefaultValueBehaviour = DefaultValueBehaviourEnum.DefaultValue;

        /// <summary>
        /// If the behaviour is set to Custom, set the default value for when the parameter is not provided
        /// </summary>
        public object CustomDefaultValue { get; set; } = null;
    }
}
