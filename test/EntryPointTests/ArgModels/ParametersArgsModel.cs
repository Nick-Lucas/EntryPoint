using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class ParametersArgsModel : BaseApplicationOptions {
        [OptionParameter(
            DoubleDashName = "param-1",
            SingleDashChar = 'a')]
        public int Param1 { get; set; }

        [OptionParameter(
            DoubleDashName = "param-2",
            SingleDashChar = 'b',
            ParameterDefaultBehaviour = ParameterDefaultEnum.CustomValue,
            ParameterDefaultValue = 7)]
        public int Param2 { get; set; }
    }
}
