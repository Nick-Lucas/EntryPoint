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
            string args = @"--hello \-world ";
            List<Token> expectedTokens = new List<Token>() {
                new Token("--hello", true),
                new Token("-world", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

        [Fact]
        // TODO: revise this behaviour, it's correct and understood
        // TODO: but maybe there is a precedent for having to escape
        // TODO: both dashes to see a potential option as a parameter?
        public void Tokenise_EscapeDoubleDash() {
            string args = @"--hello \--world ";
            List<Token> expectedTokens = new List<Token>() {
                new Token("--hello", true),
                new Token("--world", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

        [Fact]
        public void Tokenise_Quotes() {
            string args = "--goodbye \"to bob\"";
            List<Token> expectedTokens = new List<Token>() {
                new Token("--goodbye", true),
                new Token("to bob", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

        [Fact]
        public void Tokenise_EqualsSign() {
            string args = "--goodbye=bob";
            List<Token> expectedTokens = new List<Token>() {
                new Token("--goodbye", true),
                new Token("bob", false)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

        [Fact]
        public void Tokenise_Singles() {
            string args = "-abc";
            List<Token> expectedTokens = new List<Token>() {
                new Token("-a", true),
                new Token("-b", true),
                new Token("-c", true)
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

        [Fact]
        public void Tokenise_Singles_Equals() {
            string args = "-abc=bob";
            List<Token> expectedTokens = new List<Token>() {
                new Token("-a", true),
                new Token("-b", true),
                new Token("-c", true),
                new Token("bob", false),
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

        [Fact]
        public void Tokenise_Singles_Arg() {
            string args = "-abc bob";
            List<Token> expectedTokens = new List<Token>() {
                new Token("-a", true),
                new Token("-b", true),
                new Token("-c", true),
                new Token("bob", false),
            };

            var tokens = Tokeniser.MakeTokens(args);
            Assert.Equal(expectedTokens, tokens);
        }

    }
}
