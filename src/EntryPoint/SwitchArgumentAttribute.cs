using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Helpers;

namespace EntryPoint {
    public class SwitchArgumentAttribute : BaseArgumentAttribute {
        public SwitchArgumentAttribute() : base(ArgumentParserFactory.Switch) { }
    }
}
