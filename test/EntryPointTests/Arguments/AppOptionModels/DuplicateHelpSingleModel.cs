using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateHelpSingleModel : BaseCliArguments {
        [Option(LongName = "alpha", ShortName = 'h')]
        public bool Alpha { get; set; }
    }
}
