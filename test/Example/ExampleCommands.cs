using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class ExampleCommands : BaseCommands {
        [DefaultCommand]
        [Command("main")]
        [Help(
            "The Main command, for doing something")]
        public void Main(string[] args) {
            // CLI command:
            // ApplicationName -oq -re FirstItem --string Bob -n=1.2 "the operand"
            Console.WriteLine("Main Command invoked with args: ");
            Console.WriteLine(string.Join(" ", args));

            // Parses arguments based on a declarative BaseApplicationOptions implementation (below)
            MainApplicationOptions a = Cli.Parse<MainApplicationOptions>(args);
            if (a.HelpRequested) {
                Console.WriteLine(Cli.GenerateHelp<MainApplicationOptions>());
                Console.WriteLine("Enter to exit...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"a: {a.Option1}");
            Console.WriteLine($"b: {a.Option2}");
            Console.WriteLine($"c: {a.Option3}");
            Console.WriteLine($"e: {a.AppEnum}");
            Console.WriteLine($"string: {a.StringArg}");
            Console.WriteLine($"n: {a.DecimalArg}");
            Console.WriteLine($"first operand: {a.Operand1}");
            Console.WriteLine($"other operands: {string.Join(" : ", a.Operands)}");

            Console.Read();
        }

        [Command("secondary")]
        [Help(
            "The Secondary command, for doing something else. Takes only Operands")]
        public void Secondary(string[] args) {
            var options = Cli.Parse<SecondaryApplicationOptions>(args);

            Console.WriteLine("Secondary Command Invoked");

            int i = 1;
            foreach (var operand in options.Operands) {
                Console.WriteLine($"Operand {i}: {operand}");
                i++;
            }

            Console.ReadLine();
        }
        
        public override void Help(string commandsHelpText) {
            Console.WriteLine(commandsHelpText);
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
