using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Arguments.OptionStrategies;

namespace EntryPoint {

    /// <summary>
    /// Declares a positional Operand argument
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class OperandAttribute : Attribute {

        /// <summary>
        /// Declares a positional Operand argument
        /// </summary>
        /// <param name="Position">The 1-Based position of the operand</param>
        public OperandAttribute(int Position) {
            this.Position = Position;
        }

        /// <summary>
        /// The 1-Based position of the operand
        /// </summary>
        public int Position { get; set; }

    }

}
