﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPoint.Exceptions;
using EntryPointTests.Arguments.Helpers;

namespace EntryPointTests.Arguments.AppOptionModels {
    public class OperandArgsModel : BaseCliArguments {
        [Option(
            LongName: "opt-1")]
        public bool Opt1 { get; set; }

        [Option(
            LongName: "opt-2")]
        public bool Opt2 { get; set; }

        [Operand(1)]
        public string Name { get; set; } = "NoName";

        [Operand(2)]
        public string Gender { get; set; }

        [Operand(3)]
        public bool BoolValue { get; set; }

        [Operand(4)]
        public Enum1 Enum { get; set; }

        public override void OnUserFacingException(UserFacingException e, string message) {
            throw e;
        }
    }
}
