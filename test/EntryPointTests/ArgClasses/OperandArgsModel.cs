using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class OperandArgsModel : BaseArgumentsModel {
        [OptionParameter(
            DoubleDashName = "opt-param-1")]
        public int OptParam1 { get; set; }

        [OptionParameter(
            DoubleDashName = "opt-param-2")]
        public int OptParam2 { get; set; }

        [Option(
            DoubleDashName = "opt-1")]
        public bool Opt1 { get; set; }
    }
}
