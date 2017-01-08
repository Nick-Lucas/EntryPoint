#define CODE

using System;
using System.Linq;

using EntryPoint;
using System.Collections.Generic;

namespace Website {
    class article_help_generator {
        /// EntryPoint provides an automatic Help generator, which always owns the `-h` and `--help` 
        /// Options in both CliCommands and CliArguments instances.
        /// 
        /// When `--help` is invoked by the user, `.HelpInvoked` is set on CliCommands/CliArguments,  
        /// and the virtual method `OnHelpInvoked(string helpText)` is invoked. 
        /// 
        /// **By default** `OnHelpInvoked` will print the help text to screen, and call `Environment.Exit(0)`.
        /// 
        /// **By overriding** `OnHelpInvoked` on a CliCommands/CliArguments implementation,
        /// you change the implementation to something more appropriate to your program flow.
        /// 
        /// EntryPoint does not try to control your usage of this, but be aware that invoking `--help` will 
        /// disable  `Required` attributes; if you neither exit or check `.HelpInvoked`, then your 
        /// program may continue running with invalid state.
        ///
        /// ### Basic Usage
        /// 
        /// The Help Generator consumes the following information for each class type.
        /// 
        /// CliCommands
#if CODE
        [Help("This will be displayed as an initial blurb for the utility")]
        class HelpCliCommands : BaseCliCommands {

            // [DefaultCommand]
            // [Command(...)]
            [Help("Displayed as instructions for a command")]
            public void CommandMethod(string[] args) {
                // ...
            }
        }
#endif
        /// CliArguments
#if CODE
        [Help("This will be displayed as an initial blurb for the command/utility")]
        class HelpCliArguments : BaseCliArguments {
            public HelpCliArguments()
                : base(utilityName: "Displayed as the command/utility name") { }

            // [Option(...)]
            // [OptionParameter(...)]
            // [Operand(...)]
            [Help("Displayed as additional instructions for an Option/Operand")]
            public bool Value { get; set; }
        }
#endif

        /// A simple implementation would therefore look like this:
#if CODE
        [Help("This will be displayed as an initial blurb for the utility")]
        class ExampleHelpCliCommands : BaseCliCommands {

            // [DefaultCommand]
            [Command("command1")]
            [Help("Some command that can be used")]
            public void Command1(string[] args) {
                // ...etc
            }

            // This will run if --help is invoked, print help and exit the program
            public override void OnHelpInvoked(string helpText) {
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }
        }

        class CommandsHelpProgram {
            public void main(string[] args) {
                var commands = Cli.Execute<ExampleHelpCliCommands>(args);
                // Execution would not reach this point if --help is invoked, 
                // since OnHelpInvoked would run and exit the program

                // However, if you don't want to implement OnHelpInvoked, 
                // you could also do this:
                if (commands.HelpInvoked) {
                    // Return here, or run something else
                }

                // Normal Post-Command Application code...
            }
        }
#endif

        /// ...and the same thinking applies to CliArguments
#if CODE
        [Help("This will be displayed as an initial blurb for the command/utility")]
        class ExampleHelpCliArguments : BaseCliArguments {
            public ExampleHelpCliArguments()
                : base(utilityName: "Displayed as the command/utility name") { }

            [OptionParameter(LongName: "value1")]
            [Help("Some value to set")]
            public bool Value1 { get; set; }

            public override void OnHelpInvoked(string helpText) {
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }
        }

        class ArgumentsHelpProgram {
            public void main(string[] args) {
                var arguments = Cli.Parse<ExampleHelpCliArguments>(args);
                // Execution would not reach this point if --help is invoked, 
                // since OnHelpInvoked would run and exit the program

                // However, if you don't want to implement OnHelpInvoked, 
                // you could also do this:
                if (arguments.HelpInvoked) {
                    // Return here, or run something else
                }

                // Normal Application code...
            }
        }
#endif
    }
}