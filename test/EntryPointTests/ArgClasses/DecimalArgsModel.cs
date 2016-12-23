using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class DecimalArgsModel : BaseArgumentsModel {
        [OptionParameter(
            DoubleDashName = "default-zero",
            SingleDashChar = 'a')]
        public decimal DefaultZero { get; set; }

        [OptionParameter(
            DoubleDashName = "default-null",
            SingleDashChar = 'b')]
        public decimal? DefaultNull { get; set; }

        [OptionParameter(
            DoubleDashName = "default-7",
            SingleDashChar = 'c',
            NullValueBehaviour = ParameterDefaultEnum.CustomValue,
            CustomDefaultValue = 7)]
        public decimal Default7 { get; set; }
    }
}
