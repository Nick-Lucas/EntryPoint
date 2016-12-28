using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.AppOptionModels;

namespace EntryPointTests {
    public class ParametersTests {
        [Fact]
        public void NotProvided() {
            string[] args = new string[] { };

            var model = EntryPointApi.Parse<ParametersArgsModel>(args);

            Assert.StrictEqual(0, model.Param1);
            Assert.StrictEqual(7, model.Param2);
        }

        [Fact]
        public void Normal_Double() {
            string[] args = new string[] {
                "--param-1", "1",
                "--param-2", "2",
            };

            var model = EntryPointApi.Parse<ParametersArgsModel>(args);

            Assert.StrictEqual(1, model.Param1);
            Assert.StrictEqual(2, model.Param2);
        }

        [Fact]
        public void Normal_Single() {
            string[] args = new string[] {
                "-a", "1",
                "-b", "2"
            };

            var model = EntryPointApi.Parse<ParametersArgsModel>(args);

            Assert.StrictEqual(1, model.Param1);
            Assert.StrictEqual(2, model.Param2);
        }

        [Fact]
        public void CaseIncorrect_Single() {
            string[] args = new string[] {
                "-A", "1",
                "-b", "2"
            };

            Assert.Throws<UnkownOptionException>(
                () => EntryPointApi.Parse<ParametersArgsModel>(args));
        }

        [Fact]
        public void CaseIncorrect_Double() {
            string[] args = new string[] {
                "--PARAM-1", "1",
                "--param-2", "2",
            };

            var model = EntryPointApi.Parse<ParametersArgsModel>(args);

            Assert.StrictEqual(1, model.Param1);
            Assert.StrictEqual(2, model.Param2);
        }

        [Fact]
        public void MissingParam_1() {
            string[] args = new string[] {
                "--param-1",
                "--param-2", "2",
            };

            Assert.Throws<NoParameterException>(
                () => EntryPointApi.Parse<ParametersArgsModel>(args));
        }

        [Fact]
        public void MissingParam_2() {
            string[] args = new string[] {
                "--param-1",
                "--param-2", "2",
            };

            Assert.Throws<NoParameterException>(
                () => EntryPointApi.Parse<ParametersArgsModel>(args));
        }
    }
}
