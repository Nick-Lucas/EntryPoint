using EntryPoint;
using EntryPoint.Exceptions;
using EntryPointTests.Commands.BaseCommandsHelpers;
using EntryPointTests.Commands.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EntryPointTests.Commands {
    public class CommandArgumentsTests {
        [Fact]
        public void Command_RequiredArguments_HelpGenerator() {
            string[] args = {
                "Main", "--help"
            };
            Assert.Throws<Arguments.Helpers.HelpTriggeredSuccessException>(
                () => Cli.Execute<CommandModel_RequiredOptions>(args));
        }

        [Fact]
        public void Command_RequiredArguments_NotProvided() {
            string[] args = {
                "Main"
            };
            Assert.Throws<RequiredException>(
                () => Cli.Execute<CommandModel_RequiredOptions>(args));
        }

        [Fact]
        public void Command_RequiredArguments_OK() {
            string[] args = {
                "Main", "--my-option=true"
            };
            var e = Assert.Throws<CommandExecutedException>(
                () => Cli.Execute<CommandModel_RequiredOptions>(args));
            Assert.Equal(true.ToString(), e.ParamName);
        }
    }
}
