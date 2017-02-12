using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class ParametersArgsModel : BaseCliArguments {
        [OptionParameter(
            LongName: "param-1",
            ShortName: 'a')]
        public int Param1 { get; set; }

        [OptionParameter(
            LongName: "param-2",
            ShortName: 'b')]
        public int Param2 { get; set; } = 7;

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
