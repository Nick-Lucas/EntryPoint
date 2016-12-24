using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using EntryPoint.Exceptions;
using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint.Parsing {
    internal static class Parser {

        // Takes the input from the API and orchestrates the process of population
        public static A ParseAttributes<A>(A argumentsModel, List<Token> args) where A : BaseArgumentsModel {
            Model model = new Model(argumentsModel);
            ParseResult parse = GroupTokens(args, model);
            
            // Validate Model and Arguments
            ValidateModelForDuplicates(argumentsModel, model);
            ValidateTokensForDuplicateOptions(parse.TokenGroups, model);

            foreach (var tokenGroup in parse.TokenGroups) {
                var modelOption = model.FindByToken(tokenGroup.OptionToken);

                object value = modelOption.Definition.OptionParser.GetValue(modelOption, tokenGroup);
                modelOption.Property.SetValue(argumentsModel, value);
            }
            HandleUnusedOptions(argumentsModel, parse.TokenGroups, model);
            
            argumentsModel.Operands = parse.Operands.Select(t => t.Value).ToArray();
            return argumentsModel;
        }

        // if an option was not provided, which is in the ArgumentsModel
        // Validate whether it's required, then set the values to the defined defaults
        static void HandleUnusedOptions(BaseArgumentsModel argmentsModel, List<TokenGroup> tokenGroups, Model model) {
            var unusedOptions = model.WhereNotIn(tokenGroups);
            foreach (var option in unusedOptions) {
                ValidateRequiredOption(option.Property, option.Definition);

                var value = option.Definition.OptionParser.GetDefaultValue(option);
                option.Property.SetValue(argmentsModel, value);
            }
        }

        static ParseResult GroupTokens(List<Token> args, Model model) {
            var result = new ParseResult();
            var queue = new Queue<Token>(args);
            while(queue.Count > 0) {
                var token = queue.Peek();

                if (token.IsOption) {
                    queue.Dequeue();

                    bool requiresParameter = model.FindByToken(token).Definition is OptionParameterAttribute;
                    Token argument = null;
                    if (requiresParameter) {
                        AssertParameterExists(token, queue);
                        argument = queue.Dequeue();
                    }

                    result.TokenGroups.Add(new TokenGroup() {
                        OptionToken = token,
                        RequiresArgument = requiresParameter,
                        ArgumentToken = argument
                    });
                } else {
                    // If we hit a non-option, it must be an operand
                    break;
                }
            }
            result.Operands.AddRange(queue);
            return result;
        }

        static void AssertParameterExists(Token option, Queue<Token> tokensQueue) {
            if (tokensQueue.Count == 0 || tokensQueue.Peek().IsOption) {
                throw new NoParameterException(
                    $"The option {option.Value} has no parameter, but a parameter for it was expected");
            }
        }

        // If a property has a Required attribute, enforce the requirement
        static void ValidateRequiredOption(PropertyInfo prop, BaseOptionAttribute option) {
            if (prop.OptionRequired()) {
                throw new OptionRequiredException(
                    $"The option {EntryPointApi.DASH_SINGLE}{option.SingleDashChar}/"
                    + $"{EntryPointApi.DASH_DOUBLE}{option.DoubleDashName} "
                    + "was not included, but is a required option");
            }
        }

        static void ValidateTokensForDuplicateOptions(List<TokenGroup> args, Model model) {
            var duplicates = args
                .Select(a => model.FindByToken(a.OptionToken).Definition)
                .Duplicates(new BaseOptionAttributeEqualityComparer());
            if (duplicates.Any()) {
                throw new DuplicateOptionException(
                    $"Duplicate options were entered for " 
                    + $"${string.Join("/", duplicates.Select(o => o.DoubleDashName))}");
            }
        }

        static void ValidateModelForDuplicates(BaseArgumentsModel argumentsModel, Model model) {
            // Check the single dash options
            var singleDups = model
                .Where(o => o.Definition.SingleDashChar > char.MinValue)
                .Select(o => o.Definition.SingleDashChar.ToString())
                .Duplicates(StringComparer.CurrentCulture)
                .ToList();
            if (singleDups.Any()) {
                AssertDuplicateOptionsInModel(argumentsModel, singleDups);
            }
            // Check the double dash options
            var doubleDups = model
                .Where(o => o.Definition.DoubleDashName != string.Empty)
                .Select(o => o.Definition.DoubleDashName)
                .Duplicates(StringComparer.CurrentCultureIgnoreCase)
                .ToList();
            if (doubleDups.Any()) {
                AssertDuplicateOptionsInModel(argumentsModel, doubleDups);
            }
        }
        static void AssertDuplicateOptionsInModel(BaseArgumentsModel model, List<string> options) {
            throw new InvalidModelException(
                $"The given model {model.GetType().Name} was invalid. "
                + $"There are duplicate single dash arguments: {String.Join("/", options)}");
        }
    }
}
