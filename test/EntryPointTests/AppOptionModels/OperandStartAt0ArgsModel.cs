﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;
using EntryPointTests.Helpers;

namespace EntryPointTests.AppOptionModels {
    public class OperandStartAt0ArgsModel : BaseCliArguments {
        // Shou;d start at 0 :(
        [Operand(0)]
        public string Name { get; set; } = "NoName";

        [Operand(1)]
        public string Gender { get; set; }

        [Operand(2)]
        public bool BoolValue { get; set; }

        [Operand(3)]
        public Enum1 Enum { get; set; }
    }
}