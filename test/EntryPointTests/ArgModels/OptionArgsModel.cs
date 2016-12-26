using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class OptionArgsModel : BaseApplicationOptions {
        [Option(
            LongName = "my-option",
            ShortName = 'o')]
        public bool Option { get; set; }
    }
}
