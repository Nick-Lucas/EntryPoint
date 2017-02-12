using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateArgumentsModel : BaseCliArguments {
        [Option(LongName: "alpha", ShortName: 'a')]
        public bool Alpha { get; set; }

        [Option(LongName: "bravo", ShortName: 'A')]
        public bool Bravo { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
