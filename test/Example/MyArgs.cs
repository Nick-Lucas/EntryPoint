using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class MyArgs : Arguments {
        [ParameterArgument(SingleDashChar = 'n', DoubleDashName = "name")]
        public string Name { get; set; }

        [SwitchArgument(SingleDashChar = 's', DoubleDashName = "switch")]
        public string Switched { get; set; }
    }
}
