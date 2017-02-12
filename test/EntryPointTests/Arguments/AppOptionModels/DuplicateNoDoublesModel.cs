using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateNoDoublesModel : BaseCliArguments {
        [Option(LongName: "alpha")]
        public bool Alpha { get; set; }

        [OptionParameter(LongName: "beta")]
        public int Bravo { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
