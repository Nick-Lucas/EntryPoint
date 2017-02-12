using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateLimitsModel : BaseCliArguments {
        [Option(LongName: "alpha", ShortName: 'a')]
        public bool Alpha { get; set; }

        [OptionParameter(LongName: "beta", ShortName: 'A')]
        public int Bravo { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
