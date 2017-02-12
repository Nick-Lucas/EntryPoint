using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class ListsNonStructModel : BaseCliArguments {
        [OptionParameter(LongName: "class")]
        public List<List<int>> Strings { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
