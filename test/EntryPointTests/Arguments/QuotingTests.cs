using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;

namespace EntryPointTests.Arguments {
    public class QuotingTests {
        [Fact]
        public void Quoting_Standard() {
            string[] args = new string[] {
                // From:
                // --default-null "hello world"
                "--default-null", "hello world"
            };

            var model = Cli.Parse<StringArgsModel>(args);

            Assert.StrictEqual("hello world", model.DefaultNull);
        }

        [Fact]
        public void Quoting_Equals() {
            string[] args = new string[] {
                // From:
                // --default-null="hello world"
                "--default-null=hello world"
            };

            var model = Cli.Parse<StringArgsModel>(args);

            Assert.StrictEqual("hello world", model.DefaultNull);
        }

        [Fact]
        public void Quoting_List() {
            string[] args = new string[] {
                // From:
                // --default-null "hello world",goodbye
                "--default-null", "hello world,goodbye"
            };

            var model = Cli.Parse<StringArgsModel>(args);

            Assert.StrictEqual("hello world,goodbye", model.DefaultNull);
        }

        // TODO: this does not work well because .Net parses it all wrong
        // https://github.com/Nick-Lucas/EntryPoint/issues/10
        [Fact]
        public void Quoting_BrokenListTokenising() {
            string[] args = new string[] {
                // From:
                // --default-null "hello,world",goodbye
                "--default-null", "hello,world,goodbye"
            };

            var model = Cli.Parse<StringArgsModel>(args);

            Assert.StrictEqual("hello,world,goodbye", model.DefaultNull);
        }
    }
}
