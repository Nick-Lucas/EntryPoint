using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.Helpers;

namespace EntryPointTests.AppOptionModels {
    public class OperandRequiredArgsModel : BaseApplicationOptions {
        [Operand(1)]
        [Required]
        public string Name { get; set; }
    }
}
