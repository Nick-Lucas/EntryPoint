using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.ArgumentTypeParsers {
    public class ParameterParser : IArgumentType {
        internal ParameterParser() { }

        public string GetValue(string[] args, BaseArgumentAttribute definition) {
            int index = -1;

            index = definition.SingleDashIndex(args);
            if (index >= 0) {
                return GetKnownValue(args, index);
            }

            index = definition.DoubleDashIndex(args);
            if (index >= 0) {
                return GetKnownValue(args, index);
            }

            return "";
        }

        static string GetKnownValue(string[] args, int index) {
            if (args[index].Contains("=")) {
                return args[index]
                    .Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                    .Last();

            } else {
                if (args.Length == index) {
                    throw new ArgumentException($"TODO: need a proper exception here. The argument: {args[index]}, was the last argument, but a parameter for it was expected");
                }
                if (args[index + 1].StartsWith("-")) {
                    throw new Exception("TODO: Need a proper exception here. The value for the given argument was another argument, if this is a Switch then the argument should be configured that way");
                }
                return args[index + 1];
            }
        }
    }
}
