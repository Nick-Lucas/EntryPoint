using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace Example.CommandLine {
    public class ExampleCliCommands : BaseCliCommands {
        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }

        [DefaultCommand]
        [Command("primary")]
        [Help("The Main command, for doing something")]
        public void Primary(string[] args) {
            // CLI command:
            // ApplicationName -oq -re FirstItem --string Bob -n=1.2 "the operand"
            Console.WriteLine("Primary Command invoked with args: ");
            Console.WriteLine(string.Join(" ", args));

            // Parses arguments based on a declarative BaseCliArguments implementation (below)
            PrimaryCliArguments a = Cli.Parse<PrimaryCliArguments>(args);

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
        [Help("The Secondary command, for doing something else. Takes only Operands")]
        public void Secondary(string[] args) {
            Console.WriteLine("Secondary Command Invoked");

            var options = Cli.Parse<SecondaryCliArguments>(args);

            int i = 1;
            foreach (var operand in options.Operands) {
                Console.WriteLine($"Operand {i}: {operand}");
                i++;
            }

            Console.ReadLine();
        }
    }
}
