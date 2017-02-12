using EntryPoint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Help {
    public interface IHelpable {
        
        /// <summary>
        /// Set when the user invokes -h/--help
        /// </summary>
        bool HelpInvoked { get; set; }

        /// <summary>
        /// Invoked when the user invokes -h/--help
        /// </summary>
        /// <param name="helpText">The help string for this class</param>
        void OnHelpInvoked(string helpText);

    }
}
