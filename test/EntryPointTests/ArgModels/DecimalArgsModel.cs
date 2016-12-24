using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DecimalArgsModel : BaseApplicationOptions {
        [OptionParameter(
            DoubleDashName = "default-zero",
            SingleDashChar = 'a')]
        public decimal DefaultZero { get; set; }

        [OptionParameter(
            DoubleDashName = "default-null",
            SingleDashChar = 'b')]
        public decimal? DefaultNull { get; set; }

        [OptionParameter(
            DoubleDashName = "default-71",
            SingleDashChar = 'c',
            ParameterDefaultBehaviour = ParameterDefaultEnum.CustomValue,
            ParameterDefaultValue = 7.1)]
        public decimal Default71 { get; set; }
    }
}
