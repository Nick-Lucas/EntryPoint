using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;

namespace EntryPointTests.Arguments {
    public class ArgsDuplicatesTests {
        [Fact]
        public void DuplicateArgs_Single() {
            string[] args = new string[] {
                "-a", "-a"
            };

            Assert.Throws<DuplicateOptionException>(
                () => Cli.Parse<DuplicateArgumentsModel>(args));
        }
        [Fact]
        public void DuplicateArgs_Single_alt() {
            string[] args = new string[] {
                "-aa"
            };

            Assert.Throws<DuplicateOptionException>(
                () => Cli.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_Mixed() {
            string[] args = new string[] {
                "-a", "--alpha"
            };

            Assert.Throws<DuplicateOptionException>(
                () => Cli.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_Doubles() {
            string[] args = new string[] {
                "--alpha", "--alpha"
            };

            Assert.Throws<DuplicateOptionException>(
                () => Cli.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_SingleCase() {
            string[] args = new string[] {
                "-a", "-A"
            };

            Cli.Parse<DuplicateArgumentsModel>(args);
        }

        [Fact]
        public void DuplicateArgs_SingleCase_alt() {
            string[] args = new string[] {
                "-aA"
            };

            Cli.Parse<DuplicateArgumentsModel>(args);
        }

        [Fact]
        public void DuplicateArgs_DoubleCase() {
            string[] args = new string[] {
                "--alpha", "--Alpha"
            };

            Assert.Throws<DuplicateOptionException>(
                () => Cli.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_SimilarArgs_1() {
            string[] args = new string[] {
                "--log", "--log-level", "1"
            };

            Cli.Parse<DuplicateSimilarOptionsModel>(args);
        }

        [Fact]
        public void DuplicateArgs_SimilarArgs_2() {
            string[] args = new string[] {
                "--log", "--log-level=1"
            };

            Cli.Parse<DuplicateSimilarOptionsModel>(args);
        }
    }
}
