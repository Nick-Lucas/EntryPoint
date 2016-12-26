using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.OptionStrategies;

namespace EntryPoint {
    public class OperandAttribute : Attribute, IValueDefaulter {
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
        public DefaultValueBehaviourEnum DefaultValueBehaviour { get; set; } = DefaultValueBehaviourEnum.DefaultValue;

        /// <summary>
        /// If the behaviour is set to Custom, set the default value for when the parameter is not provided
        /// </summary>
        public object CustomDefaultValue { get; set; } = null;
    }
}
