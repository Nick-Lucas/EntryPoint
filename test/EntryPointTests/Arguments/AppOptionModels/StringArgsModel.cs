using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class StringArgsModel : BaseCliArguments {
        [OptionParameter(
            LongName: "default-null",
            ShortName: 'a')]
        public string DefaultNull { get; set; }

        [OptionParameter(
            LongName: "default-no-name",
            ShortName: 'b')]
        public string DefaultNoName { get; set; } = "NoName";

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
