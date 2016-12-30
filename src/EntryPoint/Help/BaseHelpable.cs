using EntryPoint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Help {
    public abstract class BaseHelpable {
        internal BaseHelpable() { }
        
        public bool HelpInvoked { get; set; }

        /// <summary>
        /// Invoked when the user invokes -h/--help
        /// </summary>
        /// <param name="helpText">The help string for this class</param>
        public virtual void OnHelpInvoked(string helpText) { }
    }
}
