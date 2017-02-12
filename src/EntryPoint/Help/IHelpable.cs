using EntryPoint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Help {
    public interface IHelpable {
        // Set when the user invokes -h/--help
        bool HelpInvoked { get; set; }

        /// <summary>
        /// Invoked when the user invokes -h/--help
        /// </summary>
        /// <param name="helpText">The help string for this class</param>
        void OnHelpInvoked(string helpText);
//{
//            Console.WriteLine(helpText);
//            Console.Write("Press enter to exit...");
//            Console.ReadLine();
//            Environment.Exit(0);
//        }
    }
}
