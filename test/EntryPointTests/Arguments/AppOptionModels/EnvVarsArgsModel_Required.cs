using EntryPoint;
using System;
using System.Collections.Generic;
using System.Text;
using EntryPoint.Exceptions;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class EnvVarsArgsModel_Required : BaseCliArguments {

        [Required]
        [EnvironmentVariable("ENV_INT")]
        public int EnvVarInt { get; set; }

        [Required]
        [EnvironmentVariable("ENV_STRING")]
        public string EnvVarString { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
