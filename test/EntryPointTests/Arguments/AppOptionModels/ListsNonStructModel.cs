using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class ListsNonStructModel : BaseCliArguments {
        [OptionParameter(LongName = "class")]
        public List<List<int>> Strings { get; set; }
    }
}
