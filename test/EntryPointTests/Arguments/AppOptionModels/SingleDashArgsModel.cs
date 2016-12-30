using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class SingleDashArgsModel : BaseCliArguments {
        [Option(
            LongName = "opt1",
            ShortName = 'a')]
        public bool Opt1 { get; set; }

        [Option(
            LongName = "opt2",
            ShortName = 'b')]
        public bool Opt2 { get; set; }

        [OptionParameter(
            LongName = "opt3",
            ShortName = 'c')]
        public string Opt3 { get; set; }
    }
}
