using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class DuplicateHelpDoubleModel : BaseApplicationOptions {
        [Option(LongName = "help", ShortName = 'a')]
        public bool Alpha { get; set; }
    }
}
