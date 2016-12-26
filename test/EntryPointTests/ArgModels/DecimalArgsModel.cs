using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class DecimalArgsModel : BaseApplicationOptions {
        [OptionParameter(
            LongName = "default-zero",
            ShortName = 'a')]
        public decimal DefaultZero { get; set; }

        [OptionParameter(
            LongName = "default-null",
            ShortName = 'b')]
        public decimal? DefaultNull { get; set; }

        [OptionParameter(
            LongName = "default-71",
            ShortName = 'c')]
        public decimal Default71 { get; set; } = 7.1m;
    }
}
