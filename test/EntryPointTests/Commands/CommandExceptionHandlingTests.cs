using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Commands;
using Xunit;
using EntryPoint;
using EntryPointTests.Commands.BaseCommandsHelpers;

namespace EntryPointTests.Commands {
    public class CommandExceptionHandlingTests {
        [Fact]
        public void CommandException_Simple() {
            Assert.Throws<DivideByZeroException>(
                () => Cli.Execute<CommandModel_ExceptionThrow>(new string[] { }));
        }

        [Fact]
        public void CommandException_InspectStacktrace() {
            var e = Assert.Throws<DivideByZeroException>(
                () => Cli.Execute<CommandModel_ExceptionThrow>(new string[] { }));

            for (int i = 1; i <= 4; i++) {
                Assert.Contains($"Layer{i}", e.StackTrace, StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}
