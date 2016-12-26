﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.ArgModels {
    public class OperandArgsModel : BaseApplicationOptions {
        [Option(
            LongName = "opt-1")]
        public bool Opt1 { get; set; }

        [Option(
            LongName = "opt-2")]
        public bool Opt2 { get; set; }

        [Operand(1)]
        public string Name { get; set; } = "NoName";

        [Operand(2)]
        public string Gender { get; set; }
    }
}
