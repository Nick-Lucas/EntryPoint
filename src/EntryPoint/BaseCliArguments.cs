using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Help;
using EntryPoint.Common;
using EntryPoint.Exceptions;

namespace EntryPoint {

    /// <summary>
    /// The base class which must be derived from for a CliArguments implementation
    /// </summary>
    public abstract class BaseCliArguments : IHelpable, IUserFacingExceptionHandler {
        
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


        // ** IHelpable **

        [Option(
            LongName: HelpRules.HelpLong, 
            ShortName: HelpRules.HelpShort)]
        [Help(
            "Displays Help information about arguments when set")]
        public bool HelpInvoked { get; set; }

        public virtual void OnHelpInvoked(string helpText) {
            Console.WriteLine(helpText);
            Console.Write("Press enter to exit...");
            Console.ReadLine();
            Environment.Exit(0);
        }


        // ** IUserFacingExceptionHandler **

        public bool UserFacingExceptionThrown { get; set; }

        public void OnUserFacingException(UserFacingException e, string message) {
            Console.WriteLine(message);
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
            Environment.Exit(1);
        }

    }

}
