using EntryPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPointTests.Arguments.Helpers;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class RequiredCliArguments : BaseCliArguments {
        public RequiredCliArguments() : base("Test") { }

        [Required]
        [OptionParameter("my-option", 'o')]
        public bool MyOption { get; set; }

        public override void OnHelpInvoked(string helpText) {
            throw new HelpTriggeredSuccessException();
        }
    }
}
