using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class OperandDumpModel : BaseCliArguments {
        [OptionParameter(
            LongName: "opt-param-1")]
        public int OptParam1 { get; set; }

        [OptionParameter(
            LongName: "opt-param-2")]
        public int OptParam2 { get; set; }

        [Option(
            LongName: "opt-1")]
        public bool Opt1 { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
