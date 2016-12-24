using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DuplicateSingleModel : BaseApplicationOptions {
        [Option(DoubleDashName = "my-double", SingleDashChar = 'm')]
        public bool MyDouble { get; set; }

        [OptionParameter(DoubleDashName = "marys-double", SingleDashChar = 'm')]
        public int MarysDouble { get; set; }
    }
}
