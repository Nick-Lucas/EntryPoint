using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class ParametersArgsModel : BaseArgumentsModel {
        [OptionParameter(
            DoubleDashName = "param-1",
            SingleDashChar = 'a')]
        public int Param1 { get; set; }

        [OptionParameter(
            DoubleDashName = "param-2",
            SingleDashChar = 'b',
            NullValueBehaviour = ParameterDefaultEnum.CustomValue,
            CustomDefaultValue = 7)]
        public int Param2 { get; set; }
    }
}
