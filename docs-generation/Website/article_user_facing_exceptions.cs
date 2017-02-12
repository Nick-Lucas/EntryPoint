#define CODE

using System;
using System.Linq;

using EntryPoint;
using System.Collections.Generic;
using EntryPoint.Exceptions;

namespace Website {
    class article_user_facing_exceptions {
        /// ## User Facing Exceptions
        /// 
        /// When a user makes a mistake, EntryPoint will throw an exception which derives from UserFacingException.
        /// 
        /// This exception is caught and the following takes place on the relevant CliCommands or CliArguments instance
        /// * `.UserFacingExceptionThrown` is set to `true`
        /// * the virtual method `.OnUserFacingException(UserFacingException e, string message)` is called
        /// 
        /// The virtual method contains a sane default implementation which prints to screen and exits the application.
        /// 
        /// 
        /// #### Custom OnUserFacingException Handler
        /// 
        /// If you want your own implementation you can override this, like so
        /// 
#if CODE
        class CliArguments : BaseCliArguments {
            public CliArguments() : base("Test") { }
            
            [Option(LongName: "Option",
                    ShortName: 'o')]
            public bool Option { get; set; }

            public override void OnUserFacingException(UserFacingException e, string message) {
                // your own handling of the message for the user
                Console.WriteLine("User error: " + message);
            }
        }

        class UserFacingExceptionProgram {
            public void main(string[] args) {
                var arguments = Cli.Parse<CliArguments>(args);
                // Execution would not reach this point if the user provides invalid arguments, 
                // since OnUserFacingException would run and exit the program

                // However, if you override OnUserFacingException and don't exit, 
                // you could also do this:
                if (arguments.UserFacingExceptionThrown) {
                    // Return here, or run something else
                }

                // Normal Post-Arguments Application code...
            }
        }
#endif

        /// #### UserFacingException Bubbling
        /// 
        /// If your application utilises both CliCommands and CliArguments, 
        /// you may want to handle UserFacingExceptions only on the CliCommands level.
        /// 
        /// This can be achieved using bubbling. 
        /// 
        /// Simply override OnUserFacingException in your CliArguments implementations and re-throw the exception.
        /// The exception will then be caught at the CliCommands level.
        /// 
#if CODE
        class BubblingCliArguments : BaseCliArguments {
            public BubblingCliArguments() : base("Test") { }

            [Option(LongName: "Option",
                    ShortName: 'o')]
            public bool Option { get; set; }

            public override void OnUserFacingException(UserFacingException e, string message) {
                // Re-throw the exception, causing it to bubble up to the root command
                throw e;
            }
        }

        class BubblingCliCommands : BaseCliCommands{
            
            [Command("command1")]
            public void Command1(string[] args) {
                var arguments = Cli.Parse<BubblingCliArguments>(args);

                // Normal Post-Arguments application logic
            }

            public override void OnUserFacingException(UserFacingException e, string message) {
                // All UserFacingException throws will now come to here
                Console.WriteLine("User error: " + message);
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        class BubblingProgram {
            public void main(string[] args) {
                var commands = Cli.Execute<BubblingCliCommands>(args);
                // Execution would not reach this point when the user provides invalid 
                // commands or arguments, since our custom OnUserFacingException handler 
                // would run and exit the program

                // However, if you override OnUserFacingException and don't exit, 
                // you could also do this:
                if (commands.UserFacingExceptionThrown) {
                    // Return here, or run something else
                }

                // Normal Post-Command Application code...
            }
        }
#endif
    }
}