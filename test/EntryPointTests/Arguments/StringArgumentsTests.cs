using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;

namespace EntryPointTests.Arguments {
    public class StringArgumentsTests {
        [Fact]
        public void NotProvided() {
            string[] args = new string[] { };

            var model = Cli.Parse<StringArgsModel>(args);

            Assert.StrictEqual(null, model.DefaultNull);
            Assert.StrictEqual("NoName", model.DefaultNoName);
        }

        [Fact]
        public void Normal() {
            string[] args = new string[] {
                "--default-null", "punch",
                "--default-no-name", "judy"
            };

            var model = Cli.Parse<StringArgsModel>(args);

            Assert.StrictEqual("punch", model.DefaultNull);
            Assert.StrictEqual("judy", model.DefaultNoName);
        }

        [Fact]
        public void NoParameter_DefaultNull() {
            string[] args = new string[] {
                "--default-null",
                "--default-no-name", "judy"
            };

            Assert.Throws<NoParameterException>(
                () => Cli.Parse<StringArgsModel>(args));
        }

        [Fact]
        public void NoParameter_DefaultNoName() {
            string[] args = new string[] {
                "--default-null", "punch",
                "--default-no-name"
            };

            Assert.Throws<NoParameterException>(
                () => Cli.Parse<StringArgsModel>(args));
        }
    }
}
