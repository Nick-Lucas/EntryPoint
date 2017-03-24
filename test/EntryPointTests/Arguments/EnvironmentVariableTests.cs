using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;
using EntryPointTests.Arguments.Helpers;

namespace EntryPointTests.Arguments {
    public class EnvironmentVariableTests {

        [Fact]
        public void NotProvided() {
            Environment.SetEnvironmentVariable("ENV_INT", null);
            Environment.SetEnvironmentVariable("ENV_STRING", null);

            var model = Cli.Parse<EnvVarsArgsModel>(new string[] { });

            Assert.StrictEqual(0, model.EnvVarInt);
            Assert.StrictEqual(null, model.EnvVarString);
        }

        [Fact]
        public void Provided() {
            Environment.SetEnvironmentVariable("ENV_INT", 1.ToString());
            Environment.SetEnvironmentVariable("ENV_STRING", "HelloWorld");

            var model = Cli.Parse<EnvVarsArgsModel>(new string[] { });

            Assert.StrictEqual(1, model.EnvVarInt);
            Assert.StrictEqual("HelloWorld", model.EnvVarString);
        }

        [Fact]
        public void IncompatibleType() {
            Environment.SetEnvironmentVariable("ENV_INT", "FAIL");
            Environment.SetEnvironmentVariable("ENV_STRING", "HelloWorld");

            Assert.True(false, "Check awaiting implementation of ValueTypeException");
            Assert.Throws<Exception>(
                () => Cli.Parse<EnvVarsArgsModel_Required>(new string[] { }));
        }

        [Fact]
        public void Required_Provided() {
            Environment.SetEnvironmentVariable("ENV_INT", 1.ToString());
            Environment.SetEnvironmentVariable("ENV_STRING", "HelloWorld");

            var model = Cli.Parse<EnvVarsArgsModel_Required>(new string[] { });

            Assert.StrictEqual(1, model.EnvVarInt);
            Assert.StrictEqual("HelloWorld", model.EnvVarString);
        }

        [Fact]
        public void Required_NotProvided_One() {
            Environment.SetEnvironmentVariable("ENV_INT", null);
            Environment.SetEnvironmentVariable("ENV_STRING", "HelloWorld");

            Assert.Throws<RequiredException>(
                () => Cli.Parse<EnvVarsArgsModel_Required>(new string[] { }));
        }

        [Fact]
        public void Required_NotProvided_Two() {
            Environment.SetEnvironmentVariable("ENV_INT", "1");
            Environment.SetEnvironmentVariable("ENV_STRING", null);

            Assert.Throws<RequiredException>(
                () => Cli.Parse<EnvVarsArgsModel_Required>(new string[] { }));
        }

        [Fact]
        public void Required_NotProvided_Two_EmptyString() {
            Environment.SetEnvironmentVariable("ENV_INT", 1.ToString());
            Environment.SetEnvironmentVariable("ENV_STRING", "");

            // I don't like this but it's a known fact that "" will count as a deleted variable
            Assert.Throws<RequiredException>(
                () => Cli.Parse<EnvVarsArgsModel_Required>(new string[] { }));
        }

        [Fact]
        public void Required_NotProvided_HelpInvoked() {
            Environment.SetEnvironmentVariable("ENV_INT", null);
            Environment.SetEnvironmentVariable("ENV_STRING", null);

            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<EnvVarsArgsModel_Required>(new string[] { "--help" }));
            Assert.Throws<HelpTriggeredSuccessException>(
                () => Cli.Parse<EnvVarsArgsModel_Required>(new string[] { "--h" }));
        }
    }
}
