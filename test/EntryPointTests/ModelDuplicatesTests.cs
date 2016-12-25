using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using Xunit;
using EntryPointTests.ArgModels;

namespace EntryPointTests {
    public class ModelDuplicatesTests {
        [Fact]
        public void Duplicates_Singles() {
            string[] args = new string[] { };

            Assert.Throws<InvalidModelException>(
                () => EntryPointApi.Parse<DuplicateSingleModel>(args));
        }

        [Fact]
        public void Duplicates_Doubles() {
            string[] args = new string[] { };

            Assert.Throws<InvalidModelException>(
                () => EntryPointApi.Parse<DuplicateDoubleModel>(args));
        }

        [Fact]
        public void Duplicates_hSingle() {
            string[] args = new string[] { };

            Assert.Throws<InvalidModelException>(
                () => EntryPointApi.Parse<DuplicateHelpSingleModel>(args));
        }

        [Fact]
        public void Duplicates_HelpDoubles() {
            string[] args = new string[] { };

            Assert.Throws<InvalidModelException>(
                () => EntryPointApi.Parse<DuplicateHelpSingleModel>(args));
        }

        [Fact]
        public void Duplicates_OnTheLimitsButFine() {
            string[] args = new string[] { };

            EntryPointApi.Parse<DuplicateLimitsModel>(args);
        }

        [Fact]
        public void Duplicates_NoSingles() {
            string[] args = new string[] { };

            EntryPointApi.Parse<DuplicateNoSinglesModel>(args);
        }

        [Fact]
        public void Duplicates_NoDoubles() {
            string[] args = new string[] { };

            EntryPointApi.Parse<DuplicateNoDoublesModel>(args);
        }
    }
}
