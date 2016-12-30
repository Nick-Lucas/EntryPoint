﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace EntryPointTests.AppOptionModels {
    public class IntegerArgsModel : BaseCliArguments {
        [OptionParameter(
            LongName = "default-zero",
            ShortName = 'a')]
        public int DefaultZero { get; set; }

        [OptionParameter(
            LongName = "default-null",
            ShortName = 'b')]
        public int? DefaultNull { get; set; }

        [OptionParameter(
            LongName = "default-7",
            ShortName = 'c')]
        public int Default7 { get; set; } = 7;

        public override void OnHelpInvoked(string helpText) {
            throw new NotImplementedException();
        }
    }
}
