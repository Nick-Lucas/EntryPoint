using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class BoolArgsModel : BaseArgumentsModel {
        [OptionParameter(
            DoubleDashName = "default-false",
            SingleDashChar = 'a')]
        public bool DefaultFalse { get; set; }

        [OptionParameter(
            DoubleDashName = "default-null",
            SingleDashChar = 'b')]
        public bool? DefaultNull { get; set; }

        [OptionParameter(
            DoubleDashName = "default-true",
            SingleDashChar = 'c',
            NullValueBehaviour = ParameterDefaultEnum.CustomValue,
            CustomDefaultValue = true)]
        public bool DefaultTrue { get; set; }
    }
}
