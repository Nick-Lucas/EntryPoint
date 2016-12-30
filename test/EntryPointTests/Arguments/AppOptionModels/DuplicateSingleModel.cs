using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class DuplicateSingleModel : BaseCliArguments {
        [Option(LongName = "my-double", ShortName = 'm')]
        public bool MyDouble { get; set; }

        [OptionParameter(LongName = "marys-double", ShortName = 'm')]
        public int MarysDouble { get; set; }
    }
}
