using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            if (!args.Any()) {

                // Supports all standard forms for CLI options
                args = new string[] {
                    "-aB",
                    "--name", "Bob",
                    "-g=male",
                    "-sy", "2" 
                };
            }

            Console.WriteLine("Parsing for:");
            Console.WriteLine(String.Join(" ", args));
            ApplicationOptions a = EntryPointApi.Parse<ApplicationOptions>(args);

            Console.WriteLine($"Name: {a.Name}");
            Console.WriteLine($"Gender: {a.Gender}");
            if (a.Student) {
                Console.WriteLine($"Study Year: {a.StudyYear}");
            }

            Console.WriteLine("Help Documentation: \n");
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
