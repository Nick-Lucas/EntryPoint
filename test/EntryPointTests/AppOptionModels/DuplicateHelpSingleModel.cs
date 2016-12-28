using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class DuplicateHelpSingleModel : BaseApplicationOptions {
        [Option(LongName = "alpha", ShortName = 'h')]
        public bool Alpha { get; set; }
    }
}
