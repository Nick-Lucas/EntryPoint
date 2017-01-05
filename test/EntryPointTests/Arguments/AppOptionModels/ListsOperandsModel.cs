using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class ListsOperandsModel : BaseCliArguments {
        [Operand(1)]
        public List<string> Strings { get; set; }
    }
}
