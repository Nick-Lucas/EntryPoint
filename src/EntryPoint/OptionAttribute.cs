using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint {

    /// <summary>
    /// Declares a standard Option argument which can either be On or Off
    /// </summary>
    public class OptionAttribute : BaseArgumentAttribute {
        public OptionAttribute() : base(ArgumentParserFactory.Option) { }
    }

}
