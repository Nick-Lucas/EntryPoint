using EntryPoint.Exceptions;
using EntryPoint.OptionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    public static class Parser {

        internal static ParseResult MakeParseResult(List<Token> tokens, Model model) {
            var result = new ParseResult();

            var queue = new Queue<Token>(tokens);
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
                        Option = token,
                        RequiresParameter = requiresParameter,
                        Parameter = argument
                    });
                } else {
                    // If we hit a non-option, it must be an operand
                    break;
                }
            }
            result.Operands.AddRange(queue);

            return result;
        }

        static void AssertParameterExists(Token token, Queue<Token> tokensQueue) {
            if (tokensQueue.Count == 0 || tokensQueue.Peek().IsOption) {
                throw new NoParameterException(
                    $"The option {token.Value} has no parameter, but a parameter for it was expected");
            }
        }
    }

}
