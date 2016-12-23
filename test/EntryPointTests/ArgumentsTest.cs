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
                "--age", "7",
                "-s"
            });

            Assert.Equal("bob", args.Name);
            Assert.StrictEqual(6.1m, args.HeightFt.Value);
            Assert.StrictEqual(7, args.Age);
            Assert.Equal(true, args.Switched);
        }

        [Fact]
        public void Parse_general_2() {
            GeneralArgs args = EntryPointApi.Parse<GeneralArgs>(new string[] {
                "--name=bob",
                "--height-ft=4.1",
                "--age=7",
            });

            Assert.Equal("bob", args.Name);
            Assert.StrictEqual(4.1m, args.HeightFt.Value);
            Assert.StrictEqual(7, args.Age);
            Assert.Equal(false, args.Switched);
        }

        [Fact]
        public void Parse_general_defaults_default() {
            GeneralArgs args = EntryPointApi.Parse<GeneralArgs>(new string[] {
            });

            Assert.Equal("NoName", args.Name);
            Assert.StrictEqual(null, args.HeightFt);
            Assert.StrictEqual(default(int), args.Age);
            Assert.Equal(false, args.Switched);
        }
    }
}
