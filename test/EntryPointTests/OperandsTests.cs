using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.ArgModels;

namespace EntryPointTests {
    public class OperandsTests {

        // Operands Mapping **

        [Fact]
        public void OperandMap_Standard() {
            var expectedOperands = new string[] { "hello", "world" };
            string[] args = new string[] {
                "--opt-1", "--opt-2",

                // Operands
                "hello", "world"
            };

            var model = EntryPointApi.Parse<OperandArgsModel>(args);

            Assert.Equal("hello", model.Name);
            Assert.Equal("world", model.Gender);
        }


        // ** Operands Dump **

        [Fact]
        public void Operands_Param() {
            var expectedOperands = new string[] { "hello", "world" };
            string[] args = new string[] {
                "--opt-param-1", "1",

                // Operands
                "hello", "world"
            };

            var model = EntryPointApi.Parse<OperandDumpModel>(args);

            Assert.True(model.Operands.All(s => expectedOperands.Contains(s)));
            Assert.True(expectedOperands.All(s => model.Operands.Contains(s)));
        }

        [Fact]
        public void Operands_Option() {
            var expectedOperands = new string[] { "hello", "world" };
            string[] args = new string[] {
                "--opt-1",

                // Operands
                "hello", "world"
            };

            var model = EntryPointApi.Parse<OperandDumpModel>(args);

            Assert.True(model.Operands.All(s => expectedOperands.Contains(s)));
            Assert.True(expectedOperands.All(s => model.Operands.Contains(s)));
        }

        [Fact]
        public void Operands_FakeOption() {
            var expectedOperands = new string[] { "--made-up", "hello", "world" };
            string[] args = new string[] {
                "--opt-1", "--made-up",

                // Operands
                "hello", "world"
            };

            Assert.Throws<UnkownOptionException>(
                () => EntryPointApi.Parse<OperandDumpModel>(args));
        }

        [Fact]
        public void AllOperands() {
            var expectedOperands = new string[] { "hello", "world" };
            string[] args = new string[] {
                // Operands
                "hello", "world"
            };

            var model = EntryPointApi.Parse<OperandDumpModel>(args);

            Assert.True(model.Operands.All(s => expectedOperands.Contains(s)));
            Assert.True(expectedOperands.All(s => model.Operands.Contains(s)));
        }

        [Fact]
        public void NoOperands_Option() {
            string[] args = new string[] {
                "--opt-1"
            };

            var model = EntryPointApi.Parse<OperandDumpModel>(args);

            Assert.False(model.Operands.Any());
        }

        [Fact]
        public void NoOperands_Param() {
            string[] args = new string[] {
                "--opt-param-1", "1"
            };

            var model = EntryPointApi.Parse<OperandDumpModel>(args);

            Assert.False(model.Operands.Any());
        }

        [Fact]
        public void Operands_NoParameters() {
            string[] args = new string[] {
            };

            var model = EntryPointApi.Parse<OperandDumpModel>(args);

            Assert.False(model.Operands.Any());
        }
    }
}
