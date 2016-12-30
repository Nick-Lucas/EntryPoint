using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Exceptions;
using System.Reflection;
using EntryPoint.Helpers;

namespace EntryPoint.Arguments {
    internal static class ArgumentReflectionExtensions {

        internal static List<Option> GetOptions(this BaseCliArguments cliArguments) {
            return cliArguments.GetType().GetRuntimeProperties()
                .Where(prop => prop.GetOptionDefinition() != null)
                .Select(prop => new Option(prop))
                .ToList();
        }

        internal static List<Operand> GetOperands(this BaseCliArguments cliArguments) {
            return cliArguments.GetType().GetRuntimeProperties()
                .Where(prop => prop.GetOperandDefinition() != null)
                .Select(prop => new Operand(prop))
                .ToList();
        }

        internal static HelpAttribute GetHelpAttribute(this BaseCliArguments cliArguments) {
            return cliArguments.GetType().GetTypeInfo()
                .GetHelp();
        }

        internal static BaseOptionAttribute GetOptionDefinition(this PropertyInfo prop) {
            var attributes = prop.GetCustomAttributes<BaseOptionAttribute>().ToList();
            if (attributes.Count > 1) {
                throw new InvalidModelException(
                    $"More than one Option attribute was applied to {prop.Name}");
            }
            if (!attributes.Any()) {
                return null;
            }
            return attributes.First();
        }

        internal static OperandAttribute GetOperandDefinition(this PropertyInfo prop) {
            return prop.GetCustomAttribute<OperandAttribute>();
        }

        internal static bool HasRequiredAttribute(this PropertyInfo prop) {
            return prop.GetCustomAttribute<RequiredAttribute>() != null;
        }
    }

}
