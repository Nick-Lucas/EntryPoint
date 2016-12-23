using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class GeneralArgs : Arguments {
        [OptionParameter(DoubleDashName = "name", SingleDashChar = 'n')]
        public string Name { get; set; }

        [Option(DoubleDashName = "switched", SingleDashChar = 's')]
        public bool Switched { get; set; }
    }
}
