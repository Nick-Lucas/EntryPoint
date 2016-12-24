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
            ValidateArgumentsForDuplicates(parse.TokenGroups, model);

            foreach (var tokenGroup in parse.TokenGroups) {
                var prop = tokenGroup.OptionToken.GetOption(model);

                ValidateRequiredOption(prop.Property, prop.Option, args);

                object value = prop.Option.OptionParser.GetValue(args, prop.Property.PropertyType, prop.Option);
                prop.Property.SetValue(argumentsModel, value);
            }
            //ValidateUnknownOption(args, options);
            argumentsModel.Operands = parse.Operands.Select(t => t.Value).ToArray();
            return argumentsModel;
        }

        static ParseResult GroupTokens(List<Token> args, Model model) {
            var result = new ParseResult();
            var queue = new Queue<Token>(args);
            while(queue.Count > 0) {
                var token = queue.Peek();

                if (token.IsOption) {
                    queue.Dequeue();

                    bool requiresParameter = token.GetOption(model).Option is OptionParameterAttribute;
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
            if (tokensQueue.Count == 0 || !tokensQueue.Peek().IsOption) {
                throw new NoParameterException(
                    $"The argument {option.Value} was the last argument, but a parameter for it was expected");
            }
        }

        // If a property has a Required attribute, enforce the requirement
        static void ValidateRequiredOption(PropertyInfo prop, BaseOptionAttribute option, List<Token> args) {
            if (prop.OptionRequired() && !args.OptionExists(option)) {
                throw new OptionRequiredException(
                    $"The option {EntryPointApi.DASH_SINGLE}{option.SingleDashChar}/"
                    + $"{EntryPointApi.DASH_DOUBLE}{option.DoubleDashName} "
                    + "was not included, but is a required option");
            }
        }

        static void ValidateArgumentsForDuplicates(List<TokenGroup> args, Model model) {
            var duplicates = args
                .Select(a => a.OptionToken.GetOption(model).Option)
                .Duplicates(new BaseOptionAttributeEqualityComparer());
            if (duplicates.Any()) {
                throw new DuplicateOptionException(
                    $"Duplicate options were entered for " 
                    + $"${string.Join("/", duplicates.Select(o => o.DoubleDashName))}");
            }
        }

        //static void ValidateUnknownOption(List<Token> args, List<BaseOptionAttribute> options) {
        //    // Validate shortfort Options
        //    foreach (var arg in args.FlattenSingles()) {
        //        if (arg.GetOption(options) == null) {
        //            AssertUnkownOption(arg);
        //        }
        //    }

        //    // Validate full Options
        //    foreach (var arg in args.FlattenDoubles()) {
        //        if (arg.GetOption(options) == null) {
        //            AssertUnkownOption(arg);
        //        }
        //    }
        //}
        //static void AssertUnkownOption(Token arg) {
        //    throw new UnkownOptionException(
        //        $"The option {EntryPointApi.DASH_SINGLE}{arg.Value} was not recognised. "
        //        + "Please ensure all given arguments are valid. Try --help");
        //}

        static void ValidateModelForDuplicates(BaseArgumentsModel argumentsModel, Model model) {
            // Check the single dash options
            var singleDups = model
                .Where(o => o.Option.SingleDashChar > char.MinValue)
                .Select(o => o.Option.SingleDashChar.ToString())
                .Duplicates(StringComparer.CurrentCulture)
                .ToList();
            if (singleDups.Any()) {
                AssertDuplicateOptionsInModel(argumentsModel, singleDups);
            }
            // Check the double dash options
            var doubleDups = model
                .Where(o => o.Option.DoubleDashName != string.Empty)
                .Select(o => o.Option.DoubleDashName)
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
