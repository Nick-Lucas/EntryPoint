using EntryPoint.Exceptions;
using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Arguments {
    internal static class ArgumentFacade {
        public static A Parse<A>(A arguments, string[] args) where A : BaseCliArguments {
            try {
                ArgumentModel model = ParseArguments(arguments, args);
                return (A)model.CliArguments;
            } catch (UserFacingException e) {
                arguments.UserFacingExceptionThrown = true;
                arguments.OnUserFacingException(e, e.Message);
            }
            return arguments;
        }

        private static ArgumentModel ParseArguments<A>(A arguments, string[] args) where A : BaseCliArguments {
            // Process inputs
            ArgumentModel model = new ArgumentModel(arguments);
            var tokens = Tokeniser.MakeTokens(args);
            ParseResult parseResult = Parser.MakeParseResult(tokens, model);

            // Map results
            model = ArgumentMapper.MapOptions(model, parseResult);

            if (model.CliArguments.HelpInvoked) {
                model.HelpFacade.Execute();
            }

            return model;
        }
    }
}
