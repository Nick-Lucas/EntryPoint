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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            if (!args.Any()) {
                args = new string[] {
                    "-aB",
                    "--name", "Bob",
                    "-g=male",
                    "-sy", "2" 
                };
            }

            // Parses arguments based on a declarative BaseApplicationOptions implementation (below)
            ApplicationOptions a = EntryPointApi.Parse<ApplicationOptions>(args);

            Console.WriteLine($"Name: {a.Name}");
            Console.WriteLine($"Gender: {a.Gender}");
            if (a.Student) {
                Console.WriteLine($"Study Year: {a.StudyYear}");
            }

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

        [OptionParameter(
            SingleDashChar = 'n', DoubleDashName = "name")]
        [Help(
            "Name of the individual")]
        public string Name { get; set; }

        [OptionParameter(
            SingleDashChar = 'g', DoubleDashName = "gender")]
        [Help(
            "Gender of the individual")]
        public string Gender { get; set; }

        [Option(
            SingleDashChar = 's', DoubleDashName = "student")]
        [Help(
            "Use this option if the individual is a student")]
        public bool Student { get; set; }

        [OptionParameter(
            SingleDashChar = 'y', DoubleDashName = "study-year",
            ParameterDefaultBehaviour = ParameterDefaultEnum.CustomValue,
            ParameterDefaultValue = -1)]
        [Help(
            "If the individual is a student, you may provide their study year")]
        public int StudyYear { get; set; }

        [Option(
            SingleDashChar = 'a', DoubleDashName = "alpha")]
        [Help(
            "A test option. Does nothing")]
        public bool Alpha { get; set; }

        [Option(
            SingleDashChar = 'B', DoubleDashName = "bravo")]
        [Help(
            "A test option. Does nothing")]
        public bool Bravo { get; set; }
    }
}

```

# Behaviour

* -O options are case sensitive
* --option-name options are case insensitive
* Standard types are all supported currently, some more advanced types are being actively worked on.
