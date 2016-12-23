using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Helpers;

namespace EntryPoint {
    public class OptionParameterAttribute : BaseArgumentAttribute {
        public OptionParameterAttribute() : base(ArgumentParserFactory.Parameter) { }
    }
}
