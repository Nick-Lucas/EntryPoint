using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint {

    /// <summary>
    /// Declares an Option argument which requires a parameter after the Option is declared
    /// </summary>
    public class OptionParameterAttribute : BaseOptionAttribute {
        public OptionParameterAttribute() : base(ArgumentParserFactory.OptionParameter) { }

        public ParameterDefaultEnum NullValueBehaviour = ParameterDefaultEnum.DefaultValue;
        public object CustomDefaultValue { get; set; } = null;
    }

}
