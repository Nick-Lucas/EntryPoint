using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class DuplicateNoDoublesModel : BaseApplicationOptions {
        [Option(LongName = "alpha")]
        public bool Alpha { get; set; }

        [OptionParameter(LongName = "beta")]
        public int Bravo { get; set; }
    }
}
