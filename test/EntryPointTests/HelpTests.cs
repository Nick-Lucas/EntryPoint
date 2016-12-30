using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

using EntryPoint;
using Xunit;
using EntryPointTests.AppOptionModels;
using EntryPointTests.Helpers;

namespace EntryPointTests {
    public class HelpTests {
        [Fact]
        public void Help_CheckRequiredDoesNotThrow_Std() {
            string[] args = new string[] {
                "--help"
            };

            // Check this doesn't throw because of Required validation
            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<HelpWithRequiredArgsModel>(args));
        }

        [Fact]
        public void Help_CheckRequiredDoesNotThrow_Operand() {
            string[] args = new string[] {
                "-r", "1", "--help"
            };

            // Check this doesn't throw because of Required validation
            // Also check it doesn't throw because of an option being included
            // Behaviour: --help will take control
            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<HelpWithRequiredArgsModel>(args));
        }

        [Fact]
        public void Help_CheckRequiredDoesNotThrow_OtherParams() {
            string[] args = new string[] {
                "-o", "name", "--help", "operand_value"
            };

            // Check this doesn't throw because of Required validation
            // Also check it doesn't throw because of an option being included
            // Behaviour: --help will take control
            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<HelpWithRequiredArgsModel>(args));
        }

        [Fact]
        public void Help_CheckRequiredDoesNotThrow_RequiredProvided() {
            string[] args = new string[] {
                "-r", "1", "--help", "operand_value"
            };

            // Check this doesn't throw because of an option being included
            // Behaviour: --help will take control
            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<HelpWithRequiredArgsModel>(args));
        }
    }
}
