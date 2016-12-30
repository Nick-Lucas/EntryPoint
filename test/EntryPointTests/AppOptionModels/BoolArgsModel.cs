using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class BoolArgsModel : BaseCliArguments {
        [OptionParameter(
            LongName = "default-false",
            ShortName = 'a')]
        public bool DefaultFalse { get; set; }

        [OptionParameter(
            LongName = "default-null",
            ShortName = 'b')]
        public bool? DefaultNull { get; set; }

        [OptionParameter(
            LongName = "default-true",
            ShortName = 'c')]
        public bool DefaultTrue { get; set; } = true;

        public override void OnHelpInvoked(string helpText) {
            throw new NotImplementedException();
        }
    }
}
