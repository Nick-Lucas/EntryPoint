using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.AppOptionModels;

namespace EntryPointTests {
    public class ArgsDuplicatesTests {
        [Fact]
        public void DuplicateArgs_Single() {
            string[] args = new string[] {
                "-a", "-a"
            };

            Assert.Throws<DuplicateOptionException>(
                () => EntryPointApi.Parse<DuplicateArgumentsModel>(args));
        }
        [Fact]
        public void DuplicateArgs_Single_alt() {
            string[] args = new string[] {
                "-aa"
            };

            Assert.Throws<DuplicateOptionException>(
                () => EntryPointApi.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_Mixed() {
            string[] args = new string[] {
                "-a", "--alpha"
            };

            Assert.Throws<DuplicateOptionException>(
                () => EntryPointApi.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_Doubles() {
            string[] args = new string[] {
                "--alpha", "--alpha"
            };

            Assert.Throws<DuplicateOptionException>(
                () => EntryPointApi.Parse<DuplicateArgumentsModel>(args));
        }

        [Fact]
        public void DuplicateArgs_SingleCase() {
            string[] args = new string[] {
                "-a", "-A"
            };

            EntryPointApi.Parse<DuplicateArgumentsModel>(args);
        }

        [Fact]
        public void DuplicateArgs_SingleCase_alt() {
            string[] args = new string[] {
                "-aA"
            };

            EntryPointApi.Parse<DuplicateArgumentsModel>(args);
        }

        [Fact]
        public void DuplicateArgs_DoubleCase() {
            string[] args = new string[] {
                "--alpha", "--Alpha"
            };

            Assert.Throws<DuplicateOptionException>(
                () => EntryPointApi.Parse<DuplicateArgumentsModel>(args));
        }
    }
}
