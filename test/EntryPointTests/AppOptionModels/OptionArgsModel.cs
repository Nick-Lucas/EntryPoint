using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class OptionArgsModel : BaseCliArguments {
        [Option(
            LongName = "my-option",
            ShortName = 'o')]
        public bool Option { get; set; }
    }
}
