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
                "-s"
            });

            Assert.Equal("bob", args.Name);
            Assert.Equal(true, args.Switched);
        }

        [Fact]
        public void Parse_general_2() {
            GeneralArgs args = EntryPointApi.Parse<GeneralArgs>(new string[] {
                "--name=bob"
            });

            Assert.Equal("bob", args.Name);
            Assert.Equal(false, args.Switched);
        }
    }
}
