using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class DuplicateDoubleModel : BaseCliArguments {
        [Option(LongName = "alpha", ShortName = 'a')]
        public bool Alpha { get; set; }

        [OptionParameter(LongName = "Alpha", ShortName = 'b')]
        public int Bravo { get; set; }

        public override void OnHelpInvoked(string helpText) {
            throw new NotImplementedException();
        }
    }
}
