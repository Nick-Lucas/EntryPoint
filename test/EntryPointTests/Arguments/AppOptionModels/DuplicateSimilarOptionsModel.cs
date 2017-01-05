using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateSimilarOptionsModel : BaseCliArguments {
        [Option(LongName = "log")]
        public bool Log { get; set; }

        [OptionParameter(LongName = "log-level")]
        public int LogLevel { get; set; }
    }
}
