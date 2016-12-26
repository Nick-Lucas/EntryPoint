using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.OptionStrategies;

namespace EntryPoint {

    /// <summary>
    /// Declares an Option argument which requires a parameter after the Option is declared
    /// </summary>
    public class OptionParameterAttribute : BaseOptionAttribute, IValueDefaultable {
        public OptionParameterAttribute() : base(OptionStrategyFactory.OptionParameter) { }

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
