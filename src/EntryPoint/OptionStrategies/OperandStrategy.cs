using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryPoint.Arguments;
using EntryPoint.Parsing;

namespace EntryPoint.OptionStrategies {
    internal class OperandStrategy {
        public object GetValue(Operand modelOperand, ParseResult parseResult) {
            int position = modelOperand.Definition.Position;
            object value = parseResult.Operands[position - 1].Value;
            return ValueConverter.ConvertValue(value, modelOperand.Property.PropertyType);
        }
    }
}
