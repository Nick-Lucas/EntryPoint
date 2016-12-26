[![Build status](https://ci.appveyor.com/api/projects/status/bocpkn9t5lhan1o9?svg=true)](https://ci.appveyor.com/project/Nick-Lucas/entrypoint)
[![NuGet](https://img.shields.io/nuget/v/EntryPoint.svg)](https://www.nuget.org/packages/EntryPoint)
[![MIT License](https://img.shields.io/github/license/Nick-Lucas/EntryPoint.svg)](https://github.com/Nick-Lucas/EntryPoint/blob/master/LICENSE)


An argument parser with a declarative and object oriented approach.

Supports:
* .Net Standard 1.6+ (All future .Net releases are built on this)
* .Net Framework 4.5.2+

Follows the [IEEE standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) closely, but does include common adblibs such as fully named --option style options.

Pull requests and suggestions are welcome, and some small tasks are already in the Issues :)

# Example

```C#
using System;
using System.Linq;

using EntryPoint;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            // CLI command:
            // ApplicationName -oq -re FirstItem --string Bob -n=1.2 "the operand"
            Console.WriteLine("For the following command: ");
            Console.WriteLine("ApplicationName -oq -re FirstItem --string Bob -n=1.2 \"the operand\"\n");
            if (!args.Any()) {
                args = new string[] {
                    // short options can be grouped
                    "-ab",

                    // the last short option in a group 
                    // can be an option parameter
                    "-ce", "FirstItem",

                    // option parameters can be whitespace or = separated
                    "--string", "Bob",
                    "-n=1.2",

                    "the operand"
                };
            }

            // Parses arguments based on a declarative BaseApplicationOptions implementation (below)
            ApplicationOptions a = EntryPointApi.Parse<ApplicationOptions>(args);

            Console.WriteLine($"a: {a.Option1}");
            Console.WriteLine($"b: {a.Option2}");
            Console.WriteLine($"c: {a.Option3}");
            Console.WriteLine($"e: {a.AppEnum}");
            Console.WriteLine($"string: {a.StringArg}");
            Console.WriteLine($"n: {a.DecimalArg}");
            Console.WriteLine($"first operand: {a.Operand1}");
            Console.WriteLine($"other operands: {string.Join(" : ", a.Operands)}");

            // Contains a built in documentation generator
            Console.WriteLine("\n\nHelp Documentation: \n");
            EntryPointApi.Parse<ApplicationOptions>(new string[] { "--help" });

            Console.Read();
        }
    }

    [Help(
        "This program is intended to show off the key features of EntryPoint, "
        + "such as this handy declarative API which includes a documentation generator")]
    public class ApplicationOptions : BaseApplicationOptions {
        public ApplicationOptions() : base("Example Project") { }

        // Simple flag options are a given
        [Option(
            ShortName = 'a', LongName = "option-1")]
        [Help(
            "A test option. Does nothing")]
        public bool Option1 { get; set; }

        // Take option parameters in any primitive type you like
        [OptionParameter(
            ShortName = 's', LongName = "string")]
        [Help(
            "Some string to be used")]
        public string StringArg { get; set; }

        // Anything can be marked as Required, and will throw if not provided
        [Required]
        [OptionParameter(
            ShortName = 'n', LongName = "number")]
        [Help(
            "Some number to be used. Must be provided")]
        public decimal DecimalArg { get; set; }

        // Also supports named and numbered enums
        [OptionParameter(
            ShortName = 'e', LongName = "app-enum")]
        [Help(
            "Provide an enum's value or name")]
        public ExampleEnum AppEnum { get; set; }

        // When not provided by the user, the default value is respected
        // This is either the Type default, or a pre-initialised value, like below.
        [OptionParameter(
            ShortName = 'd', LongName = "defaultable")]
        [Help(
            "If not provided by the user this will be defaulted")]
        public int DefaultableValue { get; set; } = -1;

        // Operands are always dumped into the BaseApplicationModel.Operands list
        // But Positional Operands can also be mapped directly
        [Operand(position: 1)]
        [Help(
            "The first Operand after all Options and OptionParameters")]
        public string Operand1 { get; set; }

        
        // These are used in the example but don't show off any new features
        
        [Option(
            ShortName = 'b', LongName = "option-2")]
        [Help(
            "A test option. Does nothing")]
        public bool Option2 { get; set; }

        [Option(
            ShortName = 'c', LongName = "option-3")]
        [Help(
            "A test option. Does nothing")]
        public bool Option3 { get; set; }

    }
}

```

# Behaviour

* -O options are case sensitive
* --option-name options are case insensitive
* Standard types are all supported currently, some more advanced types are being actively worked on.
