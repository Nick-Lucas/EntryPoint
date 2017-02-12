using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Arguments {
    internal static class ArgumentFacade {
        public static A Parse<A>(A applicationOptions, string[] args) where A : BaseCliArguments {
            // Process inputs
            ArgumentModel model = new ArgumentModel(applicationOptions);
            var tokens = Tokeniser.MakeTokens(args);
            ParseResult parseResult = Parser.MakeParseResult(tokens, model);

            // Map results
            model = ArgumentMapper.MapOptions(model, parseResult);

            if (model.CliArguments.HelpInvoked) {
                model.HelpFacade.Execute();
            }

            return (A)model.CliArguments;
        }
    }
}
