using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.Parsing {
    internal static class Mapper {

        // Takes the input from the API and orchestrates the process of population
        public static Model MapOptions(Model model, ParseResult parse) {
            
            // Validate Model and Arguments
            model.ValidateNoDuplicateNames();
            ValidateTokensForDuplicateOptions(model, parse.TokenGroups);

            // Populate ArgumentsModel
            StoreOptions(model, parse);
            HandleUnusedOptions(model, parse.TokenGroups);

            return model;
        }

        static void StoreOptions(Model model, ParseResult parse) {
            foreach (var tokenGroup in parse.TokenGroups) {
                var modelOption = model.FindByToken(tokenGroup.OptionToken);

                object value = modelOption.Definition.OptionParser.GetValue(modelOption, tokenGroup);
                modelOption.Property.SetValue(model.ApplicationOptions, value);
            }
            model.ApplicationOptions.Operands = parse.Operands.Select(t => t.Value).ToArray();
        }

        // if an option was not provided, which is in the ArgumentsModel
        // Validate whether it's required, then set the values to the defined defaults
        static void HandleUnusedOptions(Model model, List<TokenGroup> tokenGroups) {
            var unusedOptions = model.WhereNotIn(tokenGroups);
            foreach (var option in unusedOptions) {
                ValidateRequiredOption(option.Property, option.Definition);

                var value = option.Definition.OptionParser.GetDefaultValue(option);
                option.Property.SetValue(model.ApplicationOptions, value);
            }
        }

        // If a property has a Required attribute, enforce the requirement
        static void ValidateRequiredOption(PropertyInfo prop, BaseOptionAttribute option) {
            if (prop.OptionIsRequired()) {
                throw new OptionRequiredException(
                    $"The option {EntryPointApi.DASH_SINGLE}{option.SingleDashChar}/"
                    + $"{EntryPointApi.DASH_DOUBLE}{option.DoubleDashName} "
                    + "was not included, but is a required option");
            }
        }

        static void ValidateTokensForDuplicateOptions(Model model, List<TokenGroup> args) {
            var duplicates = args
                .Select(a => model.FindByToken(a.OptionToken).Definition)
                .Duplicates(new BaseOptionAttributeEqualityComparer());

            if (duplicates.Any()) {
                throw new DuplicateOptionException(
                    $"Duplicate options were entered for " 
                    + $"${string.Join("/", duplicates.Select(o => o.DoubleDashName))}");
            }
        }
    }
}
