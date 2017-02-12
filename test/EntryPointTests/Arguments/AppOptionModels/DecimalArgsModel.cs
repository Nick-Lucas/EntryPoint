using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DecimalArgsModel : BaseCliArguments {
        [OptionParameter(
            LongName: "default-zero",
            ShortName: 'a')]
        public decimal DefaultZero { get; set; }

        [OptionParameter(
            LongName: "default-null",
            ShortName: 'b')]
        public decimal? DefaultNull { get; set; }

        [OptionParameter(
            LongName: "default-71",
            ShortName: 'c')]
        public decimal Default71 { get; set; } = 7.1m;

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
