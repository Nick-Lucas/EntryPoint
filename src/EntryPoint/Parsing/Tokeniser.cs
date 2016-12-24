using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint.Parsing {
    internal static class Tokeniser {

        // Splits up a string into tokens
        public static List<Token> MakeTokens(string args) {
            StringBuilder token = new StringBuilder();
            List<Token> tokens = new List<Token>();
            bool isOption = false;
            bool quoted = false;
            bool escaped = false;
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
                    var t = token.ToString().Trim('"');
                    tokens.Add(new Token(t, isOption));
                    token.Clear();
                    isOption = false;

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
            if (token.Length > 0) {
                tokens.Add(
                    new Token(token.ToString().Trim('"'), 
                    isOption));
            }

            return tokens;
        }
    }
}
