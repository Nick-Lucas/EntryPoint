using EntryPoint.OptionParsers;
using EntryPoint.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Internals {
    /// <summary>
    /// The Base class for all argument attributes
    /// </summary>
    [AttributeUsage(
        validOn: AttributeTargets.Property, 
        AllowMultiple = true, 
        Inherited = true)]
    public class BaseOptionAttribute : Attribute {
        internal BaseOptionAttribute(IOptionParser optionParser) {
            OptionParser = optionParser;
        }
        internal IOptionParser OptionParser { get; private set; }

        /// <summary>
        /// The case sensitive character which can be declared after a - to trigger an option 
        /// </summary>
        public char SingleDashChar { get; set; }
        internal int SingleDashIndex(List<Token> args) {
            if (SingleDashChar == char.MinValue) {
                return -1;
            }

            return args.SingleDashIndex(SingleDashChar);
        }

        /// <summary>
        /// The case insensitive string which can be declared after a -- to trigger an option
        /// </summary>
        public string DoubleDashName { get; set; }
        internal int DoubleDashIndex(List<Token> args) {
            if (DoubleDashName == string.Empty) {
                return -1;
            }

            return args.DoubleDashIndex(DoubleDashName);
        }
    }
}
