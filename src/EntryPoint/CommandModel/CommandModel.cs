using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Internals;

namespace EntryPoint.CommandModel {
    public class CommandModel {
        public CommandModel(BaseCommands commandModel) {
            var methods = commandModel.GetType().GetRuntimeMethods();

            // TODO: abstract away reflection logic
            Commands = GetCommands(commandModel, methods);
            DefaultCommand = GetDefaultCommandOrNull(Commands);

            ValidateNoDuplicateNames(Commands);
            ValidateMethodArguments(Commands);
        }

        List<Command> GetCommands(BaseCommands commandModel, IEnumerable<MethodInfo> methods) {
            return methods
                    .Where(method => method.GetCustomAttribute<CommandAttribute>() != null)
                    .Select(method => new Command(commandModel, method))
                    .ToList();
        }

        Command GetDefaultCommandOrNull(List<Command> commands) {
            var defaultCommands = commands.Where(command => command.Default).ToList();
            if (defaultCommands.Count > 1) {
                AssertMultipleDefaults();
            }
            return defaultCommands.DefaultIfEmpty(null).First();
        }


        // ** Properties **

        public List<Command> Commands { get; private set; }

        // The Default Command, or Null
        public Command DefaultCommand { get; private set; }


        // ** Execution **

        public void Execute(string[] args) {
            string commandName = args.DefaultIfEmpty().First();
            Command command = Commands.FirstOrDefault(c => c.Definition.Name.Equals(commandName, StringComparison.CurrentCultureIgnoreCase));

            if (command == null) {
                if (DefaultCommand == null) {
                    throw new RequiredException(
                        $"The command {commandName} does not exist, and here is no default command");
                    
                }
                DefaultCommand.Execute(args);
            }

            command.Execute(args.Skip(1).ToArray());
        }


        // ** Validation **

        static void AssertMultipleDefaults() {
            throw new InvalidModelException(
                $"There was more than one {nameof(DefaultCommandAttribute)} "
                + $"in this CommandModel");
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
