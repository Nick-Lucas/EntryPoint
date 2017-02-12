using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateSimilarOptionsModel : BaseCliArguments {
        [Option(LongName: "log")]
        public bool Log { get; set; }

        [OptionParameter(LongName: "log-level")]
        public int LogLevel { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
