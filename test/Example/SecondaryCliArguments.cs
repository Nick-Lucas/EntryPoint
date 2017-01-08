using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class SecondaryCliArguments : BaseCliArguments {
        public SecondaryCliArguments() : base("Secondary Command") { }

        public override void OnHelpInvoked(string helpText) {
            Console.WriteLine("This help generator has been overridden");
            Console.WriteLine(helpText);
            throw new Exception("Broke flow via Exception");
        }
    }
}
