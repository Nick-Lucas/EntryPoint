using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Helpers;

namespace EntryPoint {
    public class OptionAttribute : BaseArgumentAttribute {
        public OptionAttribute() : base(ArgumentParserFactory.Switch) { }
    }
}
