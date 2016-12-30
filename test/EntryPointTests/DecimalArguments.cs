using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.AppOptionModels;

namespace EntryPointTests {
    public class DecimalArguments {
        [Fact]
        public void NotProvided() {
            string[] args = new string[] { };

            var model = Cli.Parse<DecimalArgsModel>(args);

            Assert.StrictEqual(null, model.DefaultNull);
            Assert.StrictEqual(0, model.DefaultZero);
            Assert.StrictEqual(7.1m, model.Default71);
        }

        [Fact]
        public void Normal() {
            string[] args = new string[] {
                "--default-null", "1.1",
                "--default-zero", "2.1",
                "--default-71", "3.1"
            };

            var model = Cli.Parse<DecimalArgsModel>(args);

            Assert.StrictEqual(1.1m, model.DefaultNull);
            Assert.StrictEqual(2.1m, model.DefaultZero);
            Assert.StrictEqual(3.1m, model.Default71);
        }

        [Fact]
        public void NoParameter_DefaultNull() {
            string[] args = new string[] {
                "--default-null",
                "--default-zero", "2",
                "--default-71", "3"
            };

            Assert.Throws<NoParameterException>(
                () => Cli.Parse<DecimalArgsModel>(args));
        }

        [Fact]
        public void NoParameter_DefaultNoName() {
            string[] args = new string[] {
                "--default-null", "1",
                "--default-zero", "2",
                "--default-71"
            };

            Assert.Throws<NoParameterException>(
                () => Cli.Parse<DecimalArgsModel>(args));
        }
    }
}
