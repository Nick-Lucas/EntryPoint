using System;
using System.Linq;

using EntryPoint;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            Cli.Execute<ExampleCommands>(args);
        }
    }
}
