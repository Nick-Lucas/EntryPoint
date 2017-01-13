using EntryPoint;
using Example.CommandLine;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            Cli.Execute<ExampleCliCommands>(args);
        }
    }
}
