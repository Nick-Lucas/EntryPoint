using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Arguments;
using EntryPoint.Arguments.OptionStrategies;

namespace EntryPoint {

    /// <summary>
    /// Declares a standard Option argument which can either be On or Off
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class OptionAttribute : BaseOptionAttribute {

        /// <summary>
        /// Apply to a Bool Property to mark it as an Option definition
        /// </summary>
        /// <param name="LongName">The case insensitive string which can be declared after a -- to invoke an option</param>
        /// <param name="ShortName">The case sensitive character which can be declared after a - to invoke an option</param>
        public OptionAttribute(string LongName, char ShortName) : base(OptionStrategyFactory.Option) {
            base.LongName = LongName;
            base.ShortName = ShortName;
        }

        /// <summary>
        /// Apply to a Bool Property to mark it as an Option definition
        /// </summary>
        /// <param name="LongName">The case insensitive string which can be declared after a -- to invoke an option</param>
        public OptionAttribute(string LongName) : base(OptionStrategyFactory.Option) {
            base.LongName = LongName;
        }

        /// <summary>
        /// Apply to a Bool Property to mark it as an Option definition
        /// </summary>
        /// <param name="ShortName">The case sensitive character which can be declared after a - to invoke an option</param>
        public OptionAttribute(char ShortName) : base(OptionStrategyFactory.Option) {
            base.ShortName = ShortName;
        }

    }

}
