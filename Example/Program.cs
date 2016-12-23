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
                    "--name", "bob",
                    "-s"
                };
            }

            Console.WriteLine("Parsing for:");
            Console.WriteLine(String.Join(" ", args));
            MyArgs a = EntryPointApi.Parse<MyArgs>(args);

            Console.WriteLine($"Name: {a.Name}");
            Console.WriteLine($"Switched: {a.Switched}");

            Console.Read();
        }
    }
}
