using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class DuplicateArgumentsModel : BaseCliArguments {
        [Option(LongName = "alpha", ShortName = 'a')]
        public bool Alpha { get; set; }

        [Option(LongName = "bravo", ShortName = 'A')]
        public bool Bravo { get; set; }

        public override void OnHelpInvoked(string helpText) {
            throw new NotImplementedException();
        }
    }
}
