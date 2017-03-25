using EntryPoint;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class EnvVarsArgsModel : BaseCliArguments {

        [EnvironmentVariable("ENV_INT")]
        public int EnvVarInt { get; set; }

        [EnvironmentVariable("ENV_STRING")]
        public string EnvVarString { get; set; }

        [EnvironmentVariable("ENV_STRING_DEFAULTABLE")]
        public string EnvVarStringDefaulted { get; set; } = "DEFAULT_VALUE";

    }
}
