using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Common;
using EntryPoint.Help;

namespace EntryPoint.Commands {
    internal class CommandModel {
        internal CommandModel(BaseCliCommands cliCommands) {
            CliCommands = cliCommands;
            Commands = cliCommands.GetCommands();
            DefaultCommand = GetDefaultCommandOrNull(Commands);
            HelpFacade = new HelpFacade(cliCommands);

            ValidateNoDuplicateNames(Commands);
            ValidateMethodArguments(Commands);
        }

        // If there's a [DefaultCommand] tagged command, return it. Otherwise null
        Command GetDefaultCommandOrNull(List<Command> commands) {
            var defaultCommands = commands.GetDefaultCommands();
            ValidateForMultipleDefaults(defaultCommands);
            return defaultCommands.DefaultIfEmpty(null).First();
        }


        // ** Properties **

        public BaseCliCommands CliCommands { get; private set; }
        public List<Command> Commands { get; private set; }
        public Command DefaultCommand { get; private set; }
        public HelpFacade HelpFacade { get; private set; }


        // ** Execution **

        // Starts invocation of the CommandsClass
        public void Execute(string[] args) {
            string commandName = args.DefaultIfEmpty().First();
            if (HelpRules.InvokedByArgument(commandName)) {
                HelpFacade.Execute();
            } else {
                ExecuteCommand(args, commandName);
            }
        }

        // Attempts to Execute a given Command
        void ExecuteCommand(string[] args, string commandName) {
            Command command = Commands.GetCommandToExecute(commandName);
            if (command == null) {
                ExecuteFallback(args, commandName);
            } else {
                // Pass all remaining arguments to the matched command
                command.Execute(args.Skip(1).ToArray());
            }
        }

        // Falls back on a given Default or Help
        void ExecuteFallback(string[] args, string commandName) {
            if (DefaultCommand == null) {
                // If we have no default then invoke Help 
                string message =
                    $"The command '{commandName}' does not exist, and here is no default command";
                HelpFacade.Execute(message);

            } else {
                // Pass on all arguments to the default Command
                DefaultCommand.Execute(args);
            }
        }


        // ** Validation **

        // Check that we have 1 or 0 Default Commands
        static void ValidateForMultipleDefaults(List<Command> defaults) {
            if (defaults.Count > 1) {
                throw new InvalidModelException(
                    $"There was more than one {nameof(DefaultCommandAttribute)} "
                    + $"in this CommandModel");
            }
        }

        // Check that no Commands have the same command name
        static void ValidateNoDuplicateNames(List<Command> commands) {
            var duplicates = commands
                .Select(c => c.Definition.Name)
                .Duplicates();
            if (duplicates.Any()) {
                throw new InvalidModelException(
                    $"There are duplicate command names in this {nameof(BaseCliCommands)} "
                    + $"implementation: {string.Join(", ", duplicates)}");
            }
        }

        // Check that all Command methods have a valid method signature
        static void ValidateMethodArguments(List<Command> commands) {
            foreach (var command in commands) {
                var args = command.Method.GetParameters().ToList();
                if (args.Count == 1 && args.First().ParameterType == typeof(string[])) {
                    continue;
                }
                throw new InvalidModelException(
                    $"Command {command.Method.Name} has an incorrect signature. "
                    + $"Command signature should be: `void {command.Method.Name}(string[] args) {{ ... }}`");
            }
        }
    }
}
