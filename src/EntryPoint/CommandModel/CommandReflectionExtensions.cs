using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.CommandModel;

namespace EntryPoint.CommandModel {
    internal static class CommandReflectionExtensions {
        public static List<Command> GetCommands(this BaseCommands baseCommands) {
            return baseCommands.GetType().GetRuntimeMethods()
                .Where(method => method.GetCommandDefinition() != null)
                .Select(method => new Command(baseCommands, method))
                .ToList();
        }

        public static CommandAttribute GetCommandDefinition(this MethodInfo method) {
            return method.GetCustomAttribute<CommandAttribute>();
        }

        public static List<Command> GetDefaultCommands(this List<Command> commands) {
            return commands
                .Where(command => command.Default)
                .ToList();
        }

        public static Command GetCommandToExecute(this List<Command> commands, string commandName) {
            return commands.FirstOrDefault(c => {
                return c.Definition.Name.Equals(
                    commandName, 
                    StringComparison.CurrentCultureIgnoreCase);
            });
        }
    }
}
