using EntryPoint;
using EntryPoint.Exceptions;
using EntryPointTests.Arguments.AppOptionModels;
using EntryPointTests.Arguments.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EntryPointTests.Arguments {
    public class RequiredTests {
        [Fact]
        public void RequiredProvided() {
            string[] args = {
                "--my-option=true"
            };
            var a = Cli.Parse<RequiredCliArguments>(args);
            Assert.Equal(true, a.MyOption);
        }

        [Fact]
        public void RequiredNotProvided() {
            string[] args = {
                
            };
            Assert.Throws<RequiredException>(
                () => Cli.Parse<RequiredCliArguments>(args));
        }

        [Fact]
        public void RequiredNotProvided_HelpInvoked() {
            string[] args = {
                "--help"
            };
            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<RequiredCliArguments>(args));
        }
    }
}
