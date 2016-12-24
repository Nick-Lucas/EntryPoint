using EntryPoint.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    public static class Parser {
        internal static ParseResult MakeParseResult(List<Token> args, Model model) {
            var result = new ParseResult();

            var queue = new Queue<Token>(args);
            while (queue.Count > 0) {
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
    }
}
