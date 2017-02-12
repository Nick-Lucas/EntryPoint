using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class ListsArgsModel : BaseCliArguments {
        [OptionParameter(LongName: "strings", ShortName: 's')]
        public List<string> Strings { get; set; }

        [OptionParameter(LongName: "integers", ShortName: 'i')]
        public List<int> Integers { get; set; }

        [OptionParameter(LongName: "booleans", ShortName: 'b')]
        public List<bool> Booleans { get; set; }

        [OptionParameter(LongName: "decimals")]
        public List<decimal> Decimals { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
