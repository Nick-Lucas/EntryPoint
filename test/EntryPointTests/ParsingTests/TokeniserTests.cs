using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using EntryPoint.Parsing;

namespace EntryPointTests.ParsingTests {
    public class TokeniserTests {

        [Fact]
        public void Tokenise_EscaleSingleDash() {
            string[] args = new string[] { "--hello", @"\-world" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("--hello", true),
                new Token("-world", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        [Fact]
        public void Tokenise_EscapeNonQuotedArg() {
            string[] args = new string[] { "--hello", @"\--world" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("--hello", true),
                new Token(@"\--world", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        // the whole world argument is in quotes here
        [Fact]
        public void Tokenise_EscapeQuotedArg() {
            string[] args = new string[] { "--hello", "\"--w\\orld\"" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("--hello", true),
                new Token("--world", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        [Fact]
        public void Tokenise_Quotes() {
            string[] args = new string[] { "--goodbye", "to bob" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("--goodbye", true),
                new Token("to bob", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        [Fact]
        public void Tokenise_EqualsSign() {
            string[] args = new string[] { "--goodbye=bob" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("--goodbye", true),
                new Token("bob", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        [Fact]
        public void Tokenise_Singles() {
            string[] args = new string[] { "-abc" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("-a", true),
                new Token("-b", true),
                new Token("-c", true)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        [Fact]
        public void Tokenise_Singles_Equals() {
            string[] args = new string[] { "-abc=bob" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("-a", true),
                new Token("-b", true),
                new Token("-c", true),
                new Token("bob", false),
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

        [Fact]
        public void Tokenise_Singles_Arg() {
            string[] args = new string[] { "-abc", "bob" };
            List<Token> expectedTokens = new List<Token>() {
                new Token("-a", true),
                new Token("-b", true),
                new Token("-c", true),
                new Token("bob", false),
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens, new TokenEqualityComparer());
        }

    }
}
