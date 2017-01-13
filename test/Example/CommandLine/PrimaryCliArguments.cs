using EntryPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.CommandLine {
    [Help("This program is intended to show off the key features of EntryPoint, "
        + "such as this handy declarative API which includes a documentation generator")]
    public class PrimaryCliArguments : BaseCliArguments {
        public PrimaryCliArguments() : base("Example Project") { }

        // Simple flag options are a given
        [Option(ShortName: 'a', 
                LongName: "option-1")]
        [Help("A test option. Does nothing")]
        public bool Option1 { get; set; }

        // Take option parameters in any primitive type you like
        [OptionParameter(LongName: "string")]
        [Help("Some string to be used")]
        public string StringArg { get; set; }

        // Anything can be marked as Required, and will throw if not provided
        [OptionParameter(ShortName: 'n', 
                         LongName: "number")]
        [Help("Some number to be used. Must be provided")]
        public decimal DecimalArg { get; set; }

        // Also supports named and numbered enums
        [Required]
        [OptionParameter(ShortName: 'e', 
                         LongName: "app-enum")]
        [Help("Provide an enum's value or name")]
        public ExampleEnum AppEnum { get; set; }

        // When not provided by the user, the default value is respected
        // This is either the Type default, or a pre-initialised value, like below.
        [OptionParameter(ShortName: 'd', 
                         LongName: "defaultable")]
        [Help("If not provided by the user this will be defaulted")]
        public int DefaultableValue { get; set; } = -1;

        // Operands are always dumped into the BaseApplicationModel.Operands list
        // But Positional Operands can also be mapped directly
        [Required]
        [Operand(Position: 1)]
        [Help("The first Operand after all Options and OptionParameters")]
        public string Operand1 { get; set; }


        // These are used in the example but don't show off any new features

        [Option(ShortName: 'b')]
        [Help("A test option. Does nothing")]
        public bool Option2 { get; set; }

        [Option(ShortName: 'c')]
        [Help("A test option. Does nothing")]
        public bool Option3 { get; set; }
    }
}
