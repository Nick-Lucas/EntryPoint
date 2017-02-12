using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using EntryPointTests.Arguments.Helpers;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class OperandRequiredArgsModel : BaseCliArguments {
        [Operand(1)]
        [Required]
        public string Name { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
