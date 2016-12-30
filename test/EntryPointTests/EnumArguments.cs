using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using Xunit;
using EntryPointTests.Helpers;

namespace EntryPointTests {
    public class EnumArguments {
        [Fact]
        public void Enums_Int() {
            string[] args = new string[] {
                "--opt-1", "3"
            };

            var options = Cli.Parse<EnumAppOptions>(args);

            Assert.StrictEqual(Enum1.item3, options.OptEnum1);
        }

        [Fact]
        public void Enums_Named() {
            string[] args = new string[] {
                "--opt-1", "item3"
            };

            var options = Cli.Parse<EnumAppOptions>(args);

            Assert.StrictEqual(Enum1.item3, options.OptEnum1);
        }

        [Fact]
        public void Enums_Named_IgnoresCase() {
            string[] args = new string[] {
                "--opt-1", "ITEM3"
            };

            var options = Cli.Parse<EnumAppOptions>(args);

            Assert.StrictEqual(Enum1.item3, options.OptEnum1);
        }

        [Fact]
        public void Enums_Defaults() {
            string[] args = new string[] {
            };

            var options = Cli.Parse<EnumAppOptions>(args);

            Assert.StrictEqual(default(Enum1), options.OptEnum1);
            Assert.StrictEqual(Enum1.item2, options.OptEnum2);
        }
    }

    class EnumAppOptions : BaseApplicationOptions {
        [OptionParameter(
            LongName = "opt-1")]
        public Enum1 OptEnum1 { get; set; }

        [OptionParameter(
            LongName = "opt-2")]
        public Enum1 OptEnum2 { get; set; } = Enum1.item2;
    }
}
