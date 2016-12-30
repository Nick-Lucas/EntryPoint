using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Arguments;
using EntryPoint.Arguments.OptionStrategies;

namespace EntryPoint {

    /// <summary>
    /// Declares an Option argument which requires a parameter after the Option is declared
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class OptionParameterAttribute : BaseOptionAttribute {
        public OptionParameterAttribute() : base(OptionStrategyFactory.OptionParameter) { }
    }

}
