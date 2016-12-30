using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Common;

namespace EntryPoint.Commands {
    internal static class CliCommandsHelp {
        public static string Generate(CommandModel model) {
            StringBuilder builder = new StringBuilder();

            // TODO: abstract away this reflection logic
            string appName = model.CliCommands.GetType().GetTypeInfo().Assembly.GetName().Name;
            builder.AppendLine($"Commands for {appName}");
            builder.AppendLine();
            builder.AppendLine($"Usage:\n{appName} [COMMAND] [COMMAND ARGUMENTS]");
            builder.AppendLine();
            builder.AppendLine($"For Command Help:\n{appName} [COMMAND] --help");
            builder.AppendLine();
            
            foreach (var command in model.Commands) {
                builder.AppendLine(GetCommandString(model, command));
                builder.AppendLine();
            }

            return builder.ToString();
        }

        static string GetCommandString(CommandModel model, Command command) {
            StringBuilder builder = new StringBuilder();
            builder.Append($"   {command.Definition.Name.ToUpper()}");
            if (ReferenceEquals(command, model.DefaultCommand)) {
                builder.Append(" [DEFAULT]");
            }
            builder.AppendLine();
            builder.Append($"   {command.Method.GetHelp().Detail}");
            return builder.ToString();
        }
    }
}
