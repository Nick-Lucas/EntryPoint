using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgClasses {
    public class GeneralArgs : BaseArgumentsModel {
        [OptionParameter(
            DoubleDashName = "name", 
            SingleDashChar = 'n', 
            NullValueBehaviour = ParameterDefaultEnum.CustomValue, 
            CustomDefaultValue = "NoName")]
        public string Name { get; set; }

        [OptionParameter(DoubleDashName = "height-ft", SingleDashChar = 'h')]
        public decimal? HeightFt { get; set; }

        [Option(DoubleDashName = "switched", SingleDashChar = 's')]
        public bool Switched { get; set; }
    }
}
