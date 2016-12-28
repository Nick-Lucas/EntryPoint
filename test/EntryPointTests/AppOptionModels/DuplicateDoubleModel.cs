using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class DuplicateDoubleModel : BaseApplicationOptions {
        [Option(LongName = "alpha", ShortName = 'a')]
        public bool Alpha { get; set; }

        [OptionParameter(LongName = "Alpha", ShortName = 'b')]
        public int Bravo { get; set; }
    }
}
