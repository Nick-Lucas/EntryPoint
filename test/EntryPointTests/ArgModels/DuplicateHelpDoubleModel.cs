using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DuplicateHelpDoubleModel : BaseApplicationOptions {
        [Option(DoubleDashName = "help", SingleDashChar = 'a')]
        public bool Alpha { get; set; }
    }
}
