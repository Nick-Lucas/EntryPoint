using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Helpers;

namespace EntryPoint {
    public class ParameterArgumentAttribute : BaseArgumentAttribute {
        public ParameterArgumentAttribute() : base(ArgumentParserFactory.Parameter) { }
    }
}
