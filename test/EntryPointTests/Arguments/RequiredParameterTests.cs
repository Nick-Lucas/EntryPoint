using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;

namespace EntryPointTests.Arguments {
    public class RequiredParameterTests {
        [Fact]
        public void RequiredProvided() {
            string[] args = new string[] {
                "--param-required", "1",
                "--param-optional", "2",
            };

            var model = Cli.Parse<RequiredParameterArgsModel>(args);

            Assert.StrictEqual(1, model.ParamRequired);
            Assert.StrictEqual(2, model.ParamOptional);
        }

        [Fact]
        public void RequiredMissing() {
            string[] args = new string[] {
                "--param-optional", "2",
            };

            Assert.Throws<RequiredException>(
                () => Cli.Parse<RequiredParameterArgsModel>(args));
        }
    }
}
