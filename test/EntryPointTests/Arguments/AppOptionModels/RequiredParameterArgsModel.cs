using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class RequiredParameterArgsModel : BaseCliArguments {
        [OptionParameter(
            LongName: "param-required",
            ShortName: 'r')]
        [Required]
        public int ParamRequired { get; set; }

        [OptionParameter(
            LongName: "param-optional",
            ShortName: 'o')]
        public int ParamOptional { get; set; } = 7;

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }
    }
}
