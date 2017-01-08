using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Arguments;
using EntryPoint.Arguments.OptionStrategies;

namespace EntryPoint {

    /// <summary>
    /// Declares an Option argument which requires a parameter after the Option is invoked
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class OptionParameterAttribute : BaseOptionAttribute {

        /// <summary>
        /// Apply to a Supported Property to mark it as an OptionParameter definition
        /// </summary>
        /// <param name="LongName">The case insensitive string which can be declared after a -- to invoke an option parameter</param>
        /// <param name="ShortName">The case sensitive character which can be declared after a - to invoke an option parameter </param>
        public OptionParameterAttribute(string LongName, char ShortName) : base(OptionStrategyFactory.OptionParameter) {
            base.LongName = LongName;
            base.ShortName = ShortName;
        }

        /// <summary>
        /// Apply to a Supported Property to mark it as an OptionParameter definition
        /// </summary>
        /// <param name="LongName">The case insensitive string which can be declared after a -- to invoke an option parameter</param>
        public OptionParameterAttribute(string LongName) : base(OptionStrategyFactory.OptionParameter) {
            base.LongName = LongName;
        }

        /// <summary>
        /// Apply to a Supported Property to mark it as an OptionParameter definition
        /// </summary>
        /// <param name="ShortName">The case sensitive character which can be declared after a - to invoke an option parameter</param>
        public OptionParameterAttribute(char ShortName) : base(OptionStrategyFactory.OptionParameter) {
            base.ShortName = ShortName;
        }

    }

}
