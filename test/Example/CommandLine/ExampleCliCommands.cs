using System;

using EntryPoint;

namespace Example.CommandLine {
    public class ExampleCliCommands : BaseCliCommands {
        [DefaultCommand]
        [Command("primary")]
        [Help("The Main command, for doing something")]
        public void Primary(string[] args) {
            // CLI command:
            // ApplicationName -oq -re FirstItem --string Bob -n=1.2 "the operand"
            Console.WriteLine("Primary Command invoked with args: ");
            Console.WriteLine(string.Join(" ", args));

            // Parses arguments based on a declarative BaseCliArguments implementation (below)
            PrimaryCliArguments options = Cli.Parse<PrimaryCliArguments>(args);

            Console.WriteLine($"a:                    {options.Option1}");
            Console.WriteLine($"b:                    {options.Option2}");
            Console.WriteLine($"c:                    {options.Option3}");
            Console.WriteLine($"e:                    {options.AppEnum}");
            Console.WriteLine($"string:               {options.StringArg}");
            Console.WriteLine($"n:                    {options.DecimalArg}");
            Console.WriteLine($"first operand:        {options.Operand1}");
            Console.WriteLine($"environment variable: {options.MyEnvironmentVar}");
            Console.WriteLine($"other operands:       {string.Join(" : ", options.Operands)}");
            Console.WriteLine($"defaultable value:    {options.DefaultableValue}");
            Console.WriteLine($"help invoked:         {options.HelpInvoked}");

            Console.Read();
        }

        [Command("secondary")]
        [Help("The Secondary command, for doing something else. Takes only Operands")]
        public void Secondary(string[] args) {
            Console.WriteLine("Secondary Command Invoked");

            var options = Cli.Parse<SecondaryCliArguments>(args);

            int i = 1;
            foreach (var operand in options.Operands) {
                Console.WriteLine($"operand {i}:          {operand}");
                i++;
            }
            Console.WriteLine($"help invoked:         {options.HelpInvoked}");

            Console.ReadLine();
        }
    }
}
