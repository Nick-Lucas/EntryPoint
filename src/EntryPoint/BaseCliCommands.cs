using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Commands;
using EntryPoint.Help;
using EntryPoint.Common;
using EntryPoint.Exceptions;

namespace EntryPoint {

    /// <summary>
    /// The base class which must be derived from for a CliCommands implement
    /// </summary>
    public abstract class BaseCliCommands : IHelpable, IUserFacingExceptionHandler {

        // ** IHelpable **

        public bool HelpInvoked { get; set; }

        public virtual void OnHelpInvoked(string helpText) {
            Console.WriteLine(helpText);
            Console.Write("Press enter to exit...");
            Console.ReadLine();
            Environment.Exit(0);
        }


        // ** IUserFacingExceptionHandler **

        public bool UserFacingExceptionThrown { get; set; }

        public abstract void OnUserFacingException(UserFacingException e, string message);
        //{
        //    Console.WriteLine(message);
        //    Console.WriteLine("Press enter to exit...");
        //    Console.ReadLine();
        //    Environment.Exit(1);
        //}

    }

}
