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

            Console.Read();
        }
    }

    public class ApplicationOptions : BaseApplicationOptions {
        [OptionParameter(
            SingleDashChar = 'n', DoubleDashName = "name")]
        public string Name { get; set; }

        [OptionParameter(
            SingleDashChar = 'g', DoubleDashName = "gender")]
        public string Gender { get; set; }

        [Option(
            SingleDashChar = 's', DoubleDashName = "student")]
        public bool Student { get; set; }

        [OptionParameter(
            SingleDashChar = 'y', DoubleDashName = "study-year",
            ParameterDefaultBehaviour = ParameterDefaultEnum.CustomValue,
            ParameterDefaultValue = -1)]
        public int StudyYear { get; set; }

        [Option(
            SingleDashChar = 'a', DoubleDashName = "alpha")]
        public bool Alpha { get; set; }

        [Option(
            SingleDashChar = 'B', DoubleDashName = "bravo")]
        public bool Bravo { get; set; }
    }
}
