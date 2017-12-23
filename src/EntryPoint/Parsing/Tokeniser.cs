using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntryPoint.Common;

namespace EntryPoint.Parsing {

    /// <summary>
    /// .Net Behaviour Observations
    /// 
    /// .Net automatically tokenises command line arguments and passes them to the program as `string[] args`
    /// 
    /// .Net Core does not provide raw access to the original string due to x-platform concerns,
    /// see: https://github.com/dotnet/coreclr/issues/3103
    /// This may change in the future
    /// 
    /// .Net tokenisation does a few things:
    /// * Split on Whitespace(Good)
    /// * Protects quoted contiguous text which means: 
    ///     ` "1 23",456 ` transforms to a single token: ` 1 23,456 ` (Good)
    /// * Strips quotes, even on list tokens, which means 
    ///     ` "1,23",456 ` transforms to a single token: ` 1,23,456 ` (BAD)
    /// 
    /// This behaviour needs to be worked with by the Tokeniser.
    /// 
    /// The latter behaviour is un-workable and needs to be communicated as a limitation,
    /// and communicated to the .Net team
    /// 
    /// </summary>
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

            StringBuilder token = new StringBuilder();
            List<Token> tokens = new List<Token>();
            void StoreToken() {
                if (token.Length > 0) {
                    var t = token.ToString().Trim('"');
                    tokens.Add(new Token(t, isOption));
                    token.Clear();
                    isOption = false;
                }
            };

            foreach (var c in args.ToCharArray()) {
                if (c == '=') {
                    // if char is = then store the token and start again
                    // Whitespace splitting was already handled by .Net
                    StoreToken();

                } else {
                    if (token.Length == 0 && c == '-') {
                        // Mark the new token as an option
                        isOption = true;
                    }

                    // Otherwise append this char to the current token
                    token.Append(c);
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
