using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntryPoint.OptionModel;
using EntryPoint.Parsing;

namespace EntryPoint.OptionStrategies {
    internal class OperandStrategy {
        public object GetValue(ModelOperand modelOperand, ParseResult parseResult) {
            object value;
            int position = modelOperand.Definition.Position;
            if (position <= parseResult.Operands.Count) {
                value = parseResult.Operands[position - 1].Value;
            } else {
                value = ValueConverter.CalculateDefaultValue(
                    modelOperand.Definition, 
                    modelOperand.Property.PropertyType);
            }
            return value;
        }
    }
}
