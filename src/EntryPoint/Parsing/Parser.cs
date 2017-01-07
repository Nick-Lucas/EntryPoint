using EntryPoint.Exceptions;
using EntryPoint.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryPoint.Help;

namespace EntryPoint.Parsing {
    internal static class Parser {

        internal static ParseResult MakeParseResult(List<Token> tokens, ArgumentModel model) {
            var result = new ParseResult();

            var queue = new Queue<Token>(tokens);
            while (queue.Count > 0) {
                var token = queue.Peek();

                if (token.IsOption) {
                    queue.Dequeue();
                    
                    bool takesParameter = model.FindOptionByToken(token).TakesParameter;
                    Token argument = null;
                    if (takesParameter) {
                        AssertParameterExists(token, queue);
                        argument = queue.Dequeue();
                    }

                    result.TokenGroups.Add(new TokenGroup() {
                        Option = token,
                        TakesParameter = takesParameter,
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
