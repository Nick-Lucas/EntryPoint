using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.ArgModels;

namespace EntryPointTests {
    public class RequiredParameterTests {
        [Fact]
        public void RequiredProvided() {
            string[] args = new string[] {
                "--param-required", "1",
                "--param-optional", "2",
            };

            var model = EntryPointApi.Parse<RequiredParameterArgsModel>(args);

            Assert.StrictEqual(1, model.ParamRequired);
            Assert.StrictEqual(2, model.ParamOptional);
        }

        [Fact]
        public void RequiredMissing() {
            string[] args = new string[] {
                "--param-optional", "2",
            };

            Assert.Throws<OptionRequiredException>(
                () => EntryPointApi.Parse<RequiredParameterArgsModel>(args));
        }
    }
}
