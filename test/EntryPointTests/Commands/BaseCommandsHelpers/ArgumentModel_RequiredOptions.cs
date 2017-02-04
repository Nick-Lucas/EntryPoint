using EntryPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPointTests.Commands.BaseCommandsHelpers {
    public class ArgumentModel_RequiredOptions : BaseCliArguments {
        [Required]
        [OptionParameter("my-option", 'o')]
        public bool MyOption { get; set; }

        public override void OnHelpInvoked(string helpText) {
            throw new EntryPointTests.Arguments.Helpers.HelpTriggeredSuccessException();
        }
    }
}
