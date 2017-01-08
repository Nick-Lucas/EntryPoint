using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateHelpDoubleModel : BaseCliArguments {
        [Option(LongName: "help", ShortName: 'a')]
        public bool Alpha { get; set; }
    }
}
