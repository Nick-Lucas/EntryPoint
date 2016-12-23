using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class GeneralArgs : Arguments {
        [ParameterArgument(DoubleDashName = "name", SingleDashChar = 'n')]
        public string Name { get; set; }

        [SwitchArgument(DoubleDashName = "switched", SingleDashChar = 's')]
        public bool Switched { get; set; }
    }
}
