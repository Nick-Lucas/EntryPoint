using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.Parsing {

    internal static class Tokeniser {

        // Splits up a string into tokens
        public static List<Token> MakeTokens(string args) {
            var basicTokens = BasicTokenise(args);
            return SplitCharOptions(basicTokens).ToList();
        }

        static List<Token> BasicTokenise(string args) {
            bool isOption = false;
            bool quoted = false;
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

                if (!escaped && c == '"') {
                    // If char is an unescaped quote, then flip the quoting state
                    quoted = !quoted;

                } else if (!escaped && !quoted && (Char.IsWhiteSpace(c) || c == '=')) {
                    // if char is unescaped and unquoted whitespace or = then store the token and start again
                    StoreToken();

                } else {
                    if (!escaped && !quoted && token.Length == 0 && c == '-') {
                        // Mark the new token as an option
                        isOption = true;
                    }

                    // Otherwise append to the token and start again
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
