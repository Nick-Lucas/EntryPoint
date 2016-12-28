using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.AppOptionModels;

namespace EntryPointTests {
    public class SingleDashTests {
        [Fact]
        public void Normal_Standard() {
            string[] args = new string[] {
                "-ab"
            };

            var model = EntryPointApi.Parse<SingleDashArgsModel>(args);

            Assert.StrictEqual(true, model.Opt1);
            Assert.StrictEqual(true, model.Opt2);
        }

        // IEEE standard dictates the last option in a grouped set can be an option-parameter
        [Fact]
        public void Normal_LastParam() {
            string[] args = new string[] {
                "-abc", "hello",
            };

            var model = EntryPointApi.Parse<SingleDashArgsModel>(args);

            Assert.StrictEqual(true, model.Opt1);
            Assert.StrictEqual(true, model.Opt2);
            Assert.StrictEqual("hello", model.Opt3);
        }

        // IEEE standard dictates the last option in a grouped set can be an option-parameter
        [Fact]
        public void Normal_LastParam_Equals() {
            string[] args = new string[] {
                "-abc=hello",
            };

            var model = EntryPointApi.Parse<SingleDashArgsModel>(args);

            Assert.StrictEqual(true, model.Opt1);
            Assert.StrictEqual(true, model.Opt2);
            Assert.StrictEqual("hello", model.Opt3);
        }
    }
}
