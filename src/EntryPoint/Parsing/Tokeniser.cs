using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.Parsing {

    internal static class Tokeniser {

        // Splits up a .Net Args array into tokens
        public static List<Token> MakeTokens(string[] args) {
            var basicTokens = TokeniseArgs(args).ToList();
            return SplitCharOptions(basicTokens).ToList();
        }

        // .Net Args tokenisation only splits on whitespace and strips quotes
        // So we need to split some args again, for instance on --option=123, we need two tokens
        static IEnumerable<Token> TokeniseArgs(string[] args) {
            foreach (var arg in args) {
                var tokenisedArg = BasicTokenise(arg);
                foreach (var token in tokenisedArg) {
                    yield return token;
                }
            }
        }

        static IEnumerable<Token> BasicTokenise(string args) {
            bool isOption = false;
            bool escaped = false;

            StringBuilder token = new StringBuilder();
            List<Token> tokens = new List<Token>();
            Action StoreToken = () => {
                if (token.Length > 0) {
                    var t = token.ToString().Trim('"');
                    tokens.Add(new Token(t, isOption));
                    token.Clear();
                    isOption = false;
                }
            };

            foreach (var c in args.ToCharArray()) {
                if (!escaped && c == '\\') {
                    // If char is an unescaped escape, then set the escape state and continue
                    escaped = true;
                    continue;
                }

                if (!escaped && (c == '=')) {
                    // if char is an unescaped = then store the token and start again
                    // Whitespace splitting was already handled by .Net
                    StoreToken();

                } else {
                    if (!escaped && token.Length == 0 && c == '-') {
                        // Mark the new token as an option
                        isOption = true;
                    }

                    // Otherwise append thiss char to the current token
                    token.Append(c);
                }

                if (escaped) {
                    // If this char was escape then switch off for the next char
                    escaped = false;
                }
            }
            StoreToken();

            return tokens;
        }

        // Transforms tokens like:
        // [--some-option] [-abc] [bob]
        // to:
        // [--some-option] [-a] [-b] [-c] [bob] 
        static IEnumerable<Token> SplitCharOptions(List<Token> tokens) {
            foreach (var token in tokens) {
                if (token.IsSingleDashOption()) {
                    foreach (var single in token.SplitSingleOptions()) {
                        yield return single;
                    }
                } else {
                    yield return token;
                }
            }
        }
    }

}
