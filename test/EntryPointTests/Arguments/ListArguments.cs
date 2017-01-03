using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;

namespace EntryPointTests.Arguments {
    public class ListArguments {
        [Fact]
        public void List_Strings() {
            string[] args = new string[] {
                "--strings", "one,two,three,four"
            };

            var expected = new List<string>() {
                "one", "two", "three", "four"
            };

            var model = Cli.Parse<ListsArgsModel>(args);

            Assert.Equal(expected.Count, model.Strings.Count);
            Assert.True(expected.All(s => model.Strings.Contains(s)));
        }

        [Fact]
        public void List_Ints() {
            string[] args = new string[] {
                "--strings", "1,2,3,4"
            };

            var expected = new List<int>() {
                1, 2, 3, 4
            };

            var model = Cli.Parse<ListsArgsModel>(args);

            Assert.Equal(expected.Count, model.Integers.Count);
            Assert.True(expected.All(s => model.Integers.Contains(s)));
        }

        [Fact]
        public void List_Bools() {
            string[] args = new string[] {
                "--strings", "true, false"
            };

            var expected = new List<bool>() {
                true, false
            };

            var model = Cli.Parse<ListsArgsModel>(args);

            Assert.Equal(expected.Count, model.Booleans.Count);
            Assert.True(expected.All(s => model.Booleans.Contains(s)));
        }
    }
}
