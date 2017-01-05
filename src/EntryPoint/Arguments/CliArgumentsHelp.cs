using EntryPoint.Common;
using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Arguments {
    internal static class CliArgumentsHelp {

        // Given a Model, generates a string which can be printed to the CLI 
        // based on information provided by the ApplicationModel
        public static string Generate(ArgumentModel model) {
            StringBuilder builder = new StringBuilder();
            var options = model.Options.OrderBy(mo => mo.Definition.ShortName).ToList();
            var operands = model.Operands.OrderBy(mo => mo.Definition.Position).ToList();

            // Header section
            if (model.CliArguments.UtilityName.Length > 0) {
                string utilityName = model.CliArguments.UtilityName;
                string version = MainAssembly().GetName().Version.ToString().TrimEnd('.', '0');
                builder.AppendLine($"{utilityName} v{version} Documentation");
                builder.AppendLine();
            }
            if (model.Help.Detail.Length > 0) {
                builder.AppendLine(model.Help.Detail);
                builder.AppendLine();
            }
            
            builder.AppendLine(GenerateUsageSummary(options, operands));
            builder.AppendLine();
            builder.AppendLine(GenerateBreakdown(options, operands));

            return builder.ToString();
        }

        static Assembly MainAssembly() {
            return Assembly.GetEntryAssembly();
        }

        static string GenerateUsageSummary(List<Option> options, List<Operand> operands) {
            string utilityName = MainAssembly().GetName().Name;
            return $"   Usage:\n   {utilityName} [ -o | --option ] [ -p VALUE | --parameter VALUE ] [ operands ]";
        }

        static string GenerateBreakdown(List<Option> options, List<Operand> operands) {
            StringBuilder breakdown = new StringBuilder();

            // For Options
            foreach (var option in options) {
                string shortName = "";
                if (option.Definition.ShortName > char.MinValue) {
                    shortName = $"-{option.Definition.ShortName.ToString()} ";
                }

                string longName = "";
                if (option.Definition.LongName.HasValue()) {
                    longName = $"--{option.Definition.LongName} ";
                }

                string parameterString = GetParameterString(option);
                string requiredString = option.Required.IfTrue("REQUIRED");

                breakdown.AppendLine($"   {shortName}{longName}{parameterString}{requiredString}");
                breakdown.AppendLine($"   {option.Help.Detail}");
                breakdown.AppendLine();
            }

            // For Operands
            foreach (var operand in operands) {
                string position = $"{operand.Definition.Position.ToString()} ";
                string type = GetOperandTypeString(operand);
                breakdown.AppendLine($"   [Operand {position}{type}]");
                breakdown.AppendLine($"   {operand.Help.Detail}");
            }

            return breakdown.ToString();
        }

        static string GetParameterString(Option option) {
            if (option.TakesParameter) {
                Type type = option.Property.PropertyType;
                return $"[{GetTypeSummary(type)}] ";
            }
            return "";
        }
        static string GetOperandTypeString(Operand operand) {
            Type type = operand.Property.PropertyType;
            return $"[{GetTypeSummary(type)}] ";
        }
        static string GetTypeSummary(Type type) {
            if (type.BaseType() == typeof(Enum)) {
                var names = Enum
                    .GetNames(type)
                    .Select(s => s);
                return string.Join("|", names);
            } else {
                return type.Name.ToUpper();
            }
        }

        static string GetNamesSummary(Option option) {
            bool hasShort = option.Definition.ShortName > char.MinValue;
            bool hasLong = option.Definition.LongName.Length > 0;

            if (hasShort && hasLong) {
                return $"-{option.Definition.ShortName} | --{option.Definition.LongName} ";
            } else if (hasShort) {
                return $"-{option.Definition.ShortName} ";
            } else {
                return $"--{option.Definition.LongName} ";
            }
        }
    }

}
