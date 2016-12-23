using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;

namespace EntryPointTests.ArgClasses {
    public class TypedArguments {
        [Fact]
        public void String_NotProvided() {
            string[] args = new string[] { };

            var model = EntryPointApi.Parse<StringArgsModel>(args);

            Assert.StrictEqual(null, model.DefaultNull);
            Assert.StrictEqual("NoName", model.DefaultNoName);
        }

        [Fact]
        public void String_Normal() {
            string[] args = new string[] {
                "--default-null", "punch",
                "--default-no-name", "judy"
            };

            var model = EntryPointApi.Parse<StringArgsModel>(args);

            Assert.StrictEqual("punch", model.DefaultNull);
            Assert.StrictEqual("judy", model.DefaultNoName);
        }

        [Fact]
        public void String_NoParameter_DefaultNull() {
            string[] args = new string[] {
                "--default-null",
                "--default-no-name", "judy"
            };

            Assert.Throws<NoParameterException>(
                () => EntryPointApi.Parse<StringArgsModel>(args));
        }

        [Fact]
        public void String_NoParameter_DefaultNoName() {
            string[] args = new string[] {
                "--default-null", "punch",
                "--default-no-name"
            };

            Assert.Throws<NoParameterException>(
                () => EntryPointApi.Parse<StringArgsModel>(args));
        }
    }
}
