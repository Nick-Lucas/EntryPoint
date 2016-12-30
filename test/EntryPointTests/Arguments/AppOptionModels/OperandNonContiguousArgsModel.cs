using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.Arguments.Helpers;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class OperandNonContiguousArgsModel : BaseCliArguments {
        [Operand(1)]
        public string Name { get; set; } = "NoName";

        // Gap here
        //[Operand(2)]
        //public string Gender { get; set; }

        [Operand(3)]
        public bool BoolValue { get; set; }

        [Operand(4)]
        public Enum1 Enum { get; set; }
    }
}
