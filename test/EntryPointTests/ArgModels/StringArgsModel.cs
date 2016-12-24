using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class StringArgsModel : BaseApplicationOptions {
        [OptionParameter(
            DoubleDashName = "default-null",
            SingleDashChar = 'a')]
        public string DefaultNull { get; set; }

        [OptionParameter(
            DoubleDashName = "default-no-name",
            SingleDashChar = 'b',
            ParameterDefaultBehaviour = ParameterDefaultEnum.CustomValue,
            ParameterDefaultValue = "NoName")]
        public string DefaultNoName { get; set; }
    }
}
