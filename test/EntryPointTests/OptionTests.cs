using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.AppOptionModels;

namespace EntryPointTests {
    public class OptionTests {
        [Fact]
        public void NotProvided() {
            string[] args = new string[] { };

            var model = EntryPointApi.Parse<OptionArgsModel>(args);

            Assert.StrictEqual(false, model.Option);
        }

        [Fact]
        public void Normal_Double() {
            string[] args = new string[] {
                "--my-option"
            };

            var model = EntryPointApi.Parse<OptionArgsModel>(args);

            Assert.StrictEqual(true, model.Option);
        }

        [Fact]
        public void Normal_Single() {
            string[] args = new string[] {
                "-o"
            };

            var model = EntryPointApi.Parse<OptionArgsModel>(args);

            Assert.StrictEqual(true, model.Option);
        }

        [Fact]
        public void CaseIncorrect_Single() {
            string[] args = new string[] {
                "-O"
            };

            Assert.Throws<UnkownOptionException>(
                () => EntryPointApi.Parse<OptionArgsModel>(args));
        }

        [Fact]
        public void CaseIncorrect_Double() {
            string[] args = new string[] {
                "--MY-option"
            };

            var model = EntryPointApi.Parse<OptionArgsModel>(args);

            Assert.StrictEqual(true, model.Option);
        }
    }
}
