using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;

namespace EntryPoint.ArgumentTypeParsers {
    public class OptionParameterParser : IOptionParser {
        internal OptionParameterParser() { }

        public object GetValue(string[] args, Type outputType, BaseOptionAttribute definition) {
            int index = -1;
            string value = null;

            index = definition.SingleDashIndex(args);
            if (index >= 0) {
                value = GetKnownValue(args, index);
                
            } else { 
                index = definition.DoubleDashIndex(args);
                if (index >= 0) {
                    value = GetKnownValue(args, index);
                }
            }

            return ConvertValue(value, outputType, definition);
        }

        static string GetKnownValue(string[] args, int index) {
            if (args[index].Contains("=")) {
                return args[index]
                    .Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                    .Last();

            } else {
                if (args.Length == index) {
                    throw new ArgumentException(
                        $"TODO: need a proper exception here. The argument: {args[index]}, was the last argument, but a parameter for it was expected");
                }
                if (args[index + 1].StartsWith("-")) {
                    throw new Exception(
                        "TODO: Need a proper exception here. The value for the given argument was another argument, if this is a Switch then the argument should be configured that way");
                }
                return args[index + 1];
            }
        }

        public object ConvertValue(string value, Type outputType, BaseOptionAttribute argDefinition) {
            if (value != null) {
                return Convert.ChangeType(value, outputType);
            }

            var definition = (OptionParameterAttribute)argDefinition;
            switch (definition.NullValueBehaviour) {
                case ParameterDefaultEnum.DefaultValue:
                    throw new NotImplementedException("Default value setting not fully implemented yet"); // TODO

                case ParameterDefaultEnum.CustomValue:
                    return definition.CustomDefaultValue;

                default:
                    throw new NotSupportedException(
                        $"Unsupported {nameof(ParameterDefaultEnum)} state: {definition.NullValueBehaviour}");
            }
        }
    }
}
