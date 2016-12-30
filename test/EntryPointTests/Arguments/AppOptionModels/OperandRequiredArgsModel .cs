using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.Arguments.Helpers;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class OperandRequiredArgsModel : BaseCliArguments {
        [Operand(1)]
        [Required]
        public string Name { get; set; }
    }
}
