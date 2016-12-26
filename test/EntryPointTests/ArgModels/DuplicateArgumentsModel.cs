using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DuplicateArgumentsModel : BaseApplicationOptions {
        [Option(LongName = "alpha", ShortName = 'a')]
        public bool Alpha { get; set; }

        [Option(LongName = "bravo", ShortName = 'A')]
        public bool Bravo { get; set; }
    }
}
