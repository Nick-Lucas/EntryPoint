using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.Arguments.AppOptionModels;
using EntryPointTests.Helpers;

namespace EntryPointTests.Arguments {
    public class OperandsTests {

        // Operands Mapping **

        [Fact]
        public void OperandMap_Standard() {
            string[] args = new string[] {
                "--opt-1", "--opt-2",

                // Operands
                "hello", "world"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal("hello", model.Name);
            Assert.Equal("world", model.Gender);
        }

        [Fact]
        public void OperandMap_Defaults() {
            string[] args = new string[] {
                "--opt-1", "--opt-2",
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal("NoName", model.Name);
            Assert.Equal(null, model.Gender);
        }

        [Fact]
        public void OperandMap_BoolTrue() {
            string[] args = new string[] {
                "hello", "world",

                "true"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(true, model.BoolValue);
        }

        [Fact]
        public void OperandMap_BoolTrue_2() {
            string[] args = new string[] {
                "hello", "world",

                "1"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(true, model.BoolValue);
        }

        [Fact]
        public void OperandMap_Enum_Int() {
            string[] args = new string[] {
                "hello", "world", "1",

                "2"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(Enum1.item2, model.Enum);
        }

        [Fact]
        public void OperandMap_Enum_Name() {
            string[] args = new string[] {
                "hello", "world", "1",

                "item2"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(Enum1.item2, model.Enum);
        }

        [Fact]
        public void OperandMap_RequiredAttribute_Standard() {
            string[] args = new string[] {
                "hello"
            };

            var model = Cli.Parse<OperandRequiredArgsModel>(args);

            Assert.Equal("hello", model.Name);
        }

        [Fact]
        public void OperandMap_RequiredAttribute_Error() {
            string[] args = new string[] {
            };

            Assert.Throws<RequiredException>(
                () => Cli.Parse<OperandRequiredArgsModel>(args));
        }

        [Fact]
        public void OperandMap_CheckDumpSize() {
            string[] args = new string[] { };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(0, model.Operands.Length);
        }

        [Fact]
        public void OperandMap_CheckMappedAreRemovedFromDump_One() {
            string[] args = new string[] {
                "hello"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(0, model.Operands.Length);
        }

        [Fact]
        public void OperandMap_CheckMappedAreRemovedFromDump_Max() {
            string[] args = new string[] {
                "hello", "world", "false", "item1"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(0, model.Operands.Length);
        }

        [Fact]
        public void OperandMap_CheckMappedAreRemovedFromDump_Overflow() {
            string[] args = new string[] {
                "hello", "world", "false", "item1", "something completely different"
            };

            var model = Cli.Parse<OperandArgsModel>(args);

            Assert.Equal(1, model.Operands.Length);
        }

        [Fact]
        public void OperandMap_CheckContiguityValidation() {
            string[] args = new string[] {

            };

            Assert.Throws<InvalidModelException>(
                () => Cli.Parse<OperandNonContiguousArgsModel>(args));
        }

        [Fact]
        public void OperandMap_Check0PositionValidation() {
            string[] args = new string[] {

            };

            Assert.Throws<InvalidModelException>(
                () => Cli.Parse<OperandStartAt0ArgsModel>(args));
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

            var model = Cli.Parse<OperandDumpModel>(args);

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

            var model = Cli.Parse<OperandDumpModel>(args);

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

            Assert.Throws<UnknownOptionException>(
                () => Cli.Parse<OperandDumpModel>(args));
        }

        [Fact]
        public void AllOperands() {
            var expectedOperands = new string[] { "hello", "world" };
            string[] args = new string[] {
                // Operands
                "hello", "world"
            };

            var model = Cli.Parse<OperandDumpModel>(args);

            Assert.True(model.Operands.All(s => expectedOperands.Contains(s)));
            Assert.True(expectedOperands.All(s => model.Operands.Contains(s)));
        }

        [Fact]
        public void NoOperands_Option() {
            string[] args = new string[] {
                "--opt-1"
            };

            var model = Cli.Parse<OperandDumpModel>(args);

            Assert.False(model.Operands.Any());
        }

        [Fact]
        public void NoOperands_Param() {
            string[] args = new string[] {
                "--opt-param-1", "1"
            };

            var model = Cli.Parse<OperandDumpModel>(args);

            Assert.False(model.Operands.Any());
        }

        [Fact]
        public void Operands_NoParameters() {
            string[] args = new string[] {
            };

            var model = Cli.Parse<OperandDumpModel>(args);

            Assert.False(model.Operands.Any());
        }
    }
}
