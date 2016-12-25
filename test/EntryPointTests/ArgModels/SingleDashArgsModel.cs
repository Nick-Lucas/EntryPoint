using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class SingleDashArgsModel : BaseApplicationOptions {
        [Option(
            DoubleDashName = "opt1",
            SingleDashChar = 'a')]
        public bool Opt1 { get; set; }

        [Option(
            DoubleDashName = "opt2",
            SingleDashChar = 'b')]
        public bool Opt2 { get; set; }

        [OptionParameter(
            DoubleDashName = "opt3",
            SingleDashChar = 'c')]
        public string Opt3 { get; set; }
    }
}
