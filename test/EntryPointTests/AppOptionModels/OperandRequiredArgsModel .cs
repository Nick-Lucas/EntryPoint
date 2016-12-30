using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.Helpers;

namespace EntryPointTests.AppOptionModels {
    public class OperandRequiredArgsModel : BaseCliArguments {
        [Operand(1)]
        [Required]
        public string Name { get; set; }

        public override void OnHelpInvoked(string helpText) {
            throw new NotImplementedException();
        }
    }
}
