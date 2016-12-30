using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Commands;
using EntryPoint.Common;

namespace EntryPoint.Commands {
    internal static class CommandReflectionExtensions {

        // Get a list of all commands in a BaseCliCommands class
        public static List<Command> GetCommands(this BaseCliCommands baseCommands) {
            return baseCommands.GetType().GetRuntimeMethods()
                .Where(method => method.GetCommandDefinition() != null)
                .Select(method => new Command(baseCommands, method))
                .ToList();
        }

        // Get the CommandAttribute for a given method, or null
        public static CommandAttribute GetCommandDefinition(this MethodInfo method) {
            return method.GetCustomAttribute<CommandAttribute>();
        }

        // Gets all default commands in a list of Commands
        public static List<Command> GetDefaultCommands(this List<Command> commands) {
            return commands
                .Where(command => command.Default)
                .ToList();
        }

        // Find out if a given Command method is the default Command
        public static bool HasDefaultCommandAttribute(this MethodInfo method) {
            return method.GetCustomAttribute<DefaultCommandAttribute>() != null;
        }

        // Matches a command name against a list of Commands, and returns the first match, or null
        public static Command GetCommandToExecute(this List<Command> commands, string commandName) {
            return commands.FirstOrDefault(c => {
                return c.Definition.Name.Equals(
                    commandName, 
                    StringComparison.CurrentCultureIgnoreCase);
            });
        }
    }
}
