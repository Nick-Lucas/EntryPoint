using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class RequiredParameterArgsModel : BaseApplicationOptions {
        [OptionParameter(
            DoubleDashName = "param-required",
            SingleDashChar = 'r')]
        [OptionRequired]
        public int ParamRequired { get; set; }

        [OptionParameter(
            DoubleDashName = "param-optional",
            SingleDashChar = 'o',
            ParameterDefaultBehaviour = ParameterDefaultEnum.CustomValue,
            ParameterDefaultValue = 7)]
        public int ParamOptional { get; set; }
    }
}
