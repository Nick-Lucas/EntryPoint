using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Common;
using EntryPoint.Parsing;
using EntryPoint.Arguments;

namespace EntryPoint.Arguments {
    internal static class ArgumentMapper {

        // Takes the input from the API and orchestrates the process of population
        public static ArgumentModel MapOptions(ArgumentModel model, ParseResult parseResult) {

            // Validate Model and Arguments
            model.Validate();
            ValidateTokensForDuplicateOptions(model, parseResult.TokenGroups);

            // Populate ArgumentsModel
            StoreOptions(model, parseResult);
            StoreEnvironmentVariables(model);
            HandleUnusedOptions(model, parseResult.TokenGroups);
            StoreOperands(model, parseResult);
            HandleUnusedOperands(model, parseResult);

            return model;
        }

        static void StoreOptions(ArgumentModel model, ParseResult parseResult) {
            foreach (var tokenGroup in parseResult.TokenGroups) {
                var modelOption = model.FindOptionByToken(tokenGroup.Option);

                object value = modelOption.Definition.Strategy.GetValue(modelOption, tokenGroup);
                modelOption.Property.SetValue(model.CliArguments, value);
            }
            model.CliArguments.Operands = parseResult.Operands.Select(t => t.Value).ToArray();
        }

        static void StoreEnvironmentVariables(ArgumentModel model) {
            foreach (var envVar in model.EnvironmentVariables) {
                Type type = envVar.Property.PropertyType;
                string varName = envVar.Definition.VariableName;
                object value = envVar.Strategy.GetValue(type, varName);
                envVar.Property.SetValue(model.CliArguments, value);
            }
        }

        // Map and then Remove operands which have been mapped on the Model
        static void StoreOperands(ArgumentModel model, ParseResult parseResult) {
            var operands = parseResult.Operands;
            foreach (var operand in model.Operands) {
                if (parseResult.OperandProvided(operand)) {
                    object value = operand.Strategy.GetValue(operand, parseResult);
                    operand.Property.SetValue(model.CliArguments, value);
                }
            }

            var maxPosition = model
                .Operands
                .Max(mo => mo.Definition.Position as int?) ?? 0;
            model.CliArguments.Operands = model
                .CliArguments
                .Operands
                .Skip(maxPosition)
                .ToArray();
        }

        // if an option was not provided, Validate whether it's marked as required
        static void HandleUnusedOptions(ArgumentModel model, List<TokenGroup> usedOptions) {
            if (model.CliArguments.HelpInvoked) {
                // If the help flag is set, then Required parameters are irrelevant
                return;
            }

            var requiredOption = model
                .WhereOptionsNotIn(usedOptions)
                .FirstOrDefault(mo => mo.Property.HasRequiredAttribute());

            if (requiredOption != null) {
                throw new RequiredException(
                    $"The option {Cli.DASH_SINGLE}{requiredOption.Definition.ShortName}/"
                    + $"{Cli.DASH_DOUBLE}{requiredOption.Definition.LongName} "
                    + "was not included, but is a required option");
            }
        }

        static void HandleUnusedOperands(ArgumentModel model, ParseResult parseResult) {
            if (model.CliArguments.HelpInvoked) {
                // If the help flag is set, then Required parameters are irrelevant
                return;
            }

            int providedOperandsCount = parseResult.Operands.Count;

            var requiredOperand = model.Operands
                .Where(mo => mo.Definition.Position > providedOperandsCount)
                .FirstOrDefault(mo => mo.Required);

            if (requiredOperand != null) {
                throw new RequiredException(
                    $"The operand in position {requiredOperand.Definition.Position} "
                    + "was not provided, but is required");
            }
        }

        static void ValidateTokensForDuplicateOptions(ArgumentModel model, List<TokenGroup> tokenGroups) {
            var duplicates = tokenGroups
                .Select(a => model.FindOptionByToken(a.Option).Definition)
                .Duplicates(new BaseOptionAttributeEqualityComparer());

            if (duplicates.Any()) {
                throw new DuplicateOptionException(
                    $"Duplicate options were entered for " 
                    + $"{string.Join("/", duplicates.Select(o => o.LongName))}");
            }
        }
    }
}
