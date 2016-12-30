using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Helpers;
using EntryPoint.Parsing;
using EntryPoint.Arguments;
using EntryPoint.Commands;
using EntryPoint.Interfaces;

namespace EntryPoint {

    /// <summary>
    /// The main API for EntryPoint
    /// </summary>
    public static class Cli {
        internal const string DASH_SINGLE = "-";
        internal const string DASH_DOUBLE = "--";

        // ** Parsing **

        /// <summary>
        /// Create and populate a CliArguments implementation from the command line args
        /// </summary>
        /// <typeparam name="A">The type of the CliArguments, which derives from BaseCliArguments</typeparam>
        /// <returns>A populated CliArguments instance</returns>
        public static A Parse<A>() where A : BaseCliArguments, new() {
            var args = Environment.GetCommandLineArgs();
            return Parse(new A(), args);
        }

        /// <summary>
        /// Create and populate an ApplicationOptions implementation
        /// </summary>
        /// <typeparam name="A">The type of the CliArguments, which derives from BaseCliArguments</typeparam>
        /// <param name="args">The CLI arguments input</param>
        /// <returns>A populated CliArguments instance</returns>
        public static A Parse<A>(string[] args) where A : BaseCliArguments, new() {
            return Parse(new A(), args);
        }

        /// <summary>
        /// Populate a given CliArguments instance
        /// </summary>
        /// <typeparam name="A">The type of the CliArguments, which derives from BaseCliArguments</typeparam>
        /// <param name="applicationOptions">The pre-instantiated CliArguments implementation</param>
        /// <param name="args">The CLI arguments input</param>
        /// <returns>A populated CliArguments instance</returns>
        public static A Parse<A>(A applicationOptions, string[] args) where A : BaseCliArguments {

            // Process inputs
            ArgumentModel model = new ArgumentModel(applicationOptions);
            var tokens = Tokeniser.MakeTokens(args);
            ParseResult parseResult = Parser.MakeParseResult(tokens, model);
            
            // Map results
            model = ArgumentMapper.MapOptions(model, parseResult);

            return (A)model.ApplicationOptions;
        }


        // ** Command Execution **

        /// <summary>
        /// Executes a CliCommands implementation
        /// </summary>
        /// <typeparam name="C">The type of the class implementing BaseCliCommands, which can be created with 0 arguments</typeparam>
        public static void Execute<C>() where C : BaseCliCommands, new() {
            Execute(new C(), Environment.GetCommandLineArgs());
        }

        /// <summary>
        /// Executes a CliCommands implementation
        /// </summary>
        /// <typeparam name="C">The type of the class implementing BaseCliCommands, which can be created with 0 arguments</typeparam>
        /// <param name="args">The CLI arguments input</param>
        public static void Execute<C>(string[] args) where C : BaseCliCommands, new() {
            Execute(new C(), args);
        }

        /// <summary>
        /// Executes a CliCommands implementation
        /// </summary>
        /// <typeparam name="C">The type of the class implementing BaseCliCommands</typeparam>
        /// <param name="commands">An instance of the class implementing BaseCliCommands</param>
        /// <param name="args">The CLI arguments input</param>
        public static void Execute<C>(C commands, string[] args) where C : BaseCliCommands {
            var model = new CommandModel(commands);
            model.Execute(args);
        }


        // ** Help **

        /// <summary>
        /// Generate and return a Help string for a given BaseCliArguments or BaseCliCommands instance
        /// </summary>
        /// <typeparam name="A">Custom implementation type of BaseCliArguments or BaseCliCommands which can be created with 0 arguments</typeparam>
        /// <returns>Help string</returns>
        public static string GetHelp<A>() where A : ICliHelpable, new() {
            return GetHelp(new A());
        }

        /// <summary>
        /// Generate and return a Help string for a given BaseCliArguments or BaseCliCommands instance
        /// </summary>
        /// <typeparam name="A">Custom implementation type of BaseCliArguments or BaseCliCommands</typeparam>
        /// <param name="applicationOptions">Instance of the custom BaseCliArguments or BaseCliCommands implementation</param>
        /// <returns>Help string</returns>
        public static string GetHelp<A>(A applicationOptions) where A : ICliHelpable, new() {
            if (applicationOptions is BaseCliArguments) {
                return CliArgumentsHelp.Generate(
                    new ArgumentModel(applicationOptions as BaseCliArguments));
            }
            if (applicationOptions is BaseCliCommands) {
                return CliCommandsHelp.Generate(
                    new CommandModel(applicationOptions as BaseCliCommands));
            }
            throw new InvalidOperationException(
                $"Unknown {nameof(ICliHelpable)}: "
                + $"{applicationOptions.GetType().Name}");
        }
    }

}
