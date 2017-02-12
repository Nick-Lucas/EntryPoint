using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class OptionArgsModel : BaseCliArguments {
        [Option(
            LongName: "my-option",
            ShortName: 'o')]
        public bool Option { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
