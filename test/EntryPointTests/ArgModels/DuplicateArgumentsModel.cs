using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DuplicateArgumentsModel : BaseArgumentsModel {
        [Option(DoubleDashName = "alpha", SingleDashChar = 'a')]
        public bool Alpha { get; set; }

        [Option(DoubleDashName = "bravo", SingleDashChar = 'A')]
        public bool Bravo { get; set; }
    }
}
