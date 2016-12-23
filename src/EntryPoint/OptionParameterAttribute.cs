using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint {
    public class OptionParameterAttribute : BaseArgumentAttribute {
        public OptionParameterAttribute() : base(ArgumentParserFactory.Parameter) { }
    }
}
