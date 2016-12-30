using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Arguments.OptionStrategies {

    internal static class OptionStrategyFactory {
        public static IOptionStrategy Option {
            get {
                return new OptionStrategy();
            }
        }
        public static IOptionStrategy OptionParameter {
            get {
                return new OptionParameterStrategy();
            }
        }
    }

}
