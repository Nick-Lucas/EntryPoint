using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Internals;
using EntryPoint.Parsing;
using EntryPoint.OptionModel;

namespace EntryPoint.Parsing {
    internal static class Mapper {

        // Takes the input from the API and orchestrates the process of population
        public static Model MapOptions(Model model, ParseResult parseResult) {
            
            // Validate Model and Arguments
            model.ValidateNoDuplicateNames();
            ValidateTokensForDuplicateOptions(model, parseResult.TokenGroups);

            // Populate ArgumentsModel
            StoreOptions(model, parseResult);
            HandleUnusedOptions(model, parseResult.TokenGroups);

            return model;
        }

        static void StoreOptions(Model model, ParseResult parseResult) {
            foreach (var tokenGroup in parseResult.TokenGroups) {
                var modelOption = model.FindByToken(tokenGroup.Option);

                object value = modelOption.Definition.OptionStrategy.GetValue(modelOption, tokenGroup);
                modelOption.Property.SetValue(model.ApplicationOptions, value);
            }
            model.ApplicationOptions.Operands = parseResult.Operands.Select(t => t.Value).ToArray();
        }

        // if an option was not provided, which is in the ArgumentsModel
        // Validate whether it's required, then set the values to the defined defaults
        static void HandleUnusedOptions(Model model, List<TokenGroup> usedOptions) {
            var unusedOptions = model.WhereNotIn(usedOptions);
            foreach (var option in unusedOptions) {
                ValidateRequiredOption(option.Property, option.Definition);

                var value = option.Definition.OptionStrategy.GetDefaultValue(option);
                option.Property.SetValue(model.ApplicationOptions, value);
            }
        }

        // If a property has a Required attribute, enforce the requirement
        static void ValidateRequiredOption(PropertyInfo property, BaseOptionAttribute option) {
            if (property.OptionIsRequired()) {
                throw new OptionRequiredException(
                    $"The option {EntryPointApi.DASH_SINGLE}{option.SingleDashChar}/"
                    + $"{EntryPointApi.DASH_DOUBLE}{option.DoubleDashName} "
                    + "was not included, but is a required option");
            }
        }

        static void ValidateTokensForDuplicateOptions(Model model, List<TokenGroup> tokenGroups) {
            var duplicates = tokenGroups
                .Select(a => model.FindByToken(a.Option).Definition)
                .Duplicates(new BaseOptionAttributeEqualityComparer());

            if (duplicates.Any()) {
                throw new DuplicateOptionException(
                    $"Duplicate options were entered for " 
                    + $"${string.Join("/", duplicates.Select(o => o.DoubleDashName))}");
            }
        }
    }
}
