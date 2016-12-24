using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DuplicateLimitsModel : BaseArgumentsModel {
        [Option(DoubleDashName = "alpha", SingleDashChar = 'a')]
        public bool Alpha { get; set; }

        [OptionParameter(DoubleDashName = "beta", SingleDashChar = 'A')]
        public int Bravo { get; set; }
    }
}
