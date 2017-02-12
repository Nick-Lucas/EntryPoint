using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using EntryPointTests.Commands.Helpers;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class CommandModel_ExceptionThrow : BaseCliCommands {
        [DefaultCommand]
        [Command("C1")]
        public void Command1(string[] args) {
            Layer4();
        }

        private void Layer4() {
            Layer3();
        }

        private void Layer3() {
            Layer2();
        }

        private void Layer2() {
            Layer1();
        }

        private void Layer1() {
            throw new DivideByZeroException();
        }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
