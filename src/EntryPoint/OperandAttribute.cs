using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.OptionStrategies;

namespace EntryPoint {
    public class OperandAttribute : Attribute {
        public OperandAttribute(int position) {
            Position = position;
        }

        /// <summary>
        /// The 1 Based position of the operand
        /// </summary>
        public int Position { get; set; }
    }
}
