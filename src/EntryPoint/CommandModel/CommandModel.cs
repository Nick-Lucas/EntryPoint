using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Internals;

namespace EntryPoint.CommandModel {
    public class CommandModel {
        public CommandModel(BaseCommands baseCommands) {
            Commands = baseCommands.GetCommands();
            DefaultCommand = GetDefaultCommandOrNull(Commands);

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

        public List<Command> Commands { get; private set; }
        public Command DefaultCommand { get; private set; }


        // ** Execution **

        public void Execute(string[] args) {
            string commandName = args.DefaultIfEmpty().First();
            Command command = Commands.GetCommandToExecute(commandName);
            if (command == null) {
                // If we have no default then throw
                if (DefaultCommand == null) {
                    throw new RequiredException(
                        $"The command {commandName} does not exist, and here is no default command");
                    
                }

                // Pass on all arguments to the default Command
                DefaultCommand.Execute(args);
            } else {
                // Pass all remaining arguments to the matched command
                command.Execute(args.Skip(1).ToArray());
            }
        }


        // ** Validation **

        static void ValidateForMultipleDefaults(List<Command> defaults) {
            if (defaults.Count > 1) {
                throw new InvalidModelException(
                    $"There was more than one {nameof(DefaultCommandAttribute)} "
                    + $"in this CommandModel");
            }
        }

        static void ValidateNoDuplicateNames(List<Command> commands) {
            var duplicates = commands
                .Select(c => c.Definition.Name)
                .Duplicates();
            if (duplicates.Any()) {
                throw new InvalidModelException(
                    $"There are duplicate command names in this {nameof(BaseCommands)} "
                    + $"implementation: {string.Join(", ", duplicates)}");
            }
        }

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
