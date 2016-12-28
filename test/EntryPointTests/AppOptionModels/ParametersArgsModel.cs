using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class ParametersArgsModel : BaseApplicationOptions {
        [OptionParameter(
            LongName = "param-1",
            ShortName = 'a')]
        public int Param1 { get; set; }

        [OptionParameter(
            LongName = "param-2",
            ShortName = 'b')]
        public int Param2 { get; set; } = 7;
    }
}
