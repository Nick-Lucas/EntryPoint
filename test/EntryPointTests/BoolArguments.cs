using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.AppOptionModels;

namespace EntryPointTests {
    public class BoolArguments {
        [Fact]
        public void NotProvided() {
            string[] args = new string[] { };

            var model = EntryPointApi.Parse<BoolArgsModel>(args);

            Assert.StrictEqual(null, model.DefaultNull);
            Assert.StrictEqual(false, model.DefaultFalse);
            Assert.StrictEqual(true, model.DefaultTrue);
        }

        [Fact]
        public void Normal() {
            string[] args = new string[] {
                "--default-null", "true",
                "--default-false", "true",
                "--default-true", "0"
            };

            var model = EntryPointApi.Parse<BoolArgsModel>(args);

            Assert.StrictEqual(true, model.DefaultNull);
            Assert.StrictEqual(true, model.DefaultFalse);
            Assert.StrictEqual(false, model.DefaultTrue);
        }

        [Fact]
        public void NoParameter_DefaultNull() {
            string[] args = new string[] {
                "--default-null", 
                "--default-false", "true",
                "--default-true", "true"
            };

            Assert.Throws<NoParameterException>(
                () => EntryPointApi.Parse<BoolArgsModel>(args));
        }

        [Fact]
        public void NoParameter_DefaultNoName() {
            string[] args = new string[] {
                "--default-false", "true",
                "--default-null", "true",
                "--default-true"
            };

            Assert.Throws<NoParameterException>(
                () => EntryPointApi.Parse<BoolArgsModel>(args));
        }
    }
}
