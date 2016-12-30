using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class Commands : BaseCommands {
        [DefaultCommand]
        [Command("main")]
        public void Main(string[] args) {
            // CLI command:
            // ApplicationName -oq -re FirstItem --string Bob -n=1.2 "the operand"
            Console.WriteLine("Main Command invoked with args: ");
            Console.WriteLine(string.Join(" ", args));

            // Parses arguments based on a declarative BaseApplicationOptions implementation (below)
            MainApplicationOptions a = EntryPointApi.Parse<MainApplicationOptions>(args);
            if (a.HelpRequested) {
                Console.WriteLine(EntryPointApi.GenerateHelp<MainApplicationOptions>());
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
        public void Secondary(string[] args) {
            var options = EntryPointApi.Parse<SecondaryApplicationOptions>(args);

            Console.WriteLine("Secondary Command Invoked");

            int i = 1;
            foreach (var operand in options.Operands) {
                Console.WriteLine($"Operand {i}: {operand}");
                i++;
            }

            Console.ReadLine();
        }
    }
}
