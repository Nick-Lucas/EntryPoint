using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.ArgClasses;
using Xunit;

namespace EntryPointTests {
    public class ArgumentsTest {

        [Fact]
        public void Parse_general_1() {
            GeneralArgs args = EntryPointApi.Parse<GeneralArgs>(new string[] {
                "--name", "bob",
                "--height-ft", "6.1",
                "-s"
            });

            Assert.Equal("bob", args.Name);
            Assert.StrictEqual(6.1m, args.HeightFt.Value);
            Assert.Equal(true, args.Switched);
        }

        [Fact]
        public void Parse_general_2() {
            GeneralArgs args = EntryPointApi.Parse<GeneralArgs>(new string[] {
                "--name=bob",
                "--height-ft=4.1",
            });

            Assert.Equal("bob", args.Name);
            Assert.StrictEqual(4.1m, args.HeightFt.Value);
            Assert.Equal(false, args.Switched);
        }

        [Fact]
        public void Parse_general_defaults_default() {
            GeneralArgs args = EntryPointApi.Parse<GeneralArgs>(new string[] {
            });

            Assert.Equal(default(string), args.Name);
            Assert.StrictEqual(null, args.HeightFt);
            Assert.Equal(false, args.Switched);
        }
    }
}
