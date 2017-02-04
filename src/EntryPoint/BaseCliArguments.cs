using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Help;
using EntryPoint.Common;

namespace EntryPoint {

    /// <summary>
    /// The base class which must be derived from for a CliArguments implementation
    /// </summary>
    public abstract class BaseCliArguments : BaseHelpable {
        /// <summary>
        /// The base class which must be derived from for  CliArguments implementation
        /// </summary>
        /// <param name="utilityName">The name of your utility or application</param>
        public BaseCliArguments(string utilityName) {
            UtilityName = utilityName;
        }
        internal BaseCliArguments() : this("") { }

        internal string UtilityName { get; set; }

        /// <summary>
        /// All trailing arguments left after any Options and OptionParameters defined in CliArguments
        /// </summary>
        public string[] Operands { get; internal set; }

        [Option(
            LongName: HelpRules.HelpLong, 
            ShortName: HelpRules.HelpShort)]
        [Help(
            "Displays Help information about arguments when set")]
        public new bool HelpInvoked { get; set; }
    }

}
