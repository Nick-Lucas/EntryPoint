using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DuplicateNoDoublesModel : BaseArgumentsModel {
        [Option(DoubleDashName = "alpha")]
        public bool Alpha { get; set; }

        [OptionParameter(DoubleDashName = "beta")]
        public int Bravo { get; set; }
    }
}
