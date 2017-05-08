using System;
using EntryPoint.Parsing;

namespace EntryPoint.Arguments.OptionStrategies
{
    internal class OptionStrategy : IOptionStrategy
    {
        public object GetValue(Option modelOption, TokenGroup tokenGroup)
        {
            var value = HasDoubleOption(tokenGroup.Option)
                     || HasSingleOption(tokenGroup.Option, modelOption.Definition.ShortName);
            return CheckValue(value, modelOption.Property.PropertyType, modelOption.Definition);
        }

        public bool RequiresParameter => false;

        // ** Helpers **

        bool HasSingleOption(Token arg, char? argName)
        {
            return argName != null && arg.IsSingleDashOption();
        }

        bool HasDoubleOption(Token arg)
        {
            return arg.IsDoubleDashOption();
        }

        object CheckValue(bool value, Type outputType, BaseOptionAttribute definition)
        {
            if (outputType != typeof(bool))
            {
                throw new InvalidOperationException(
                    $"The type of {Cli.DASH_DOUBLE}{definition.LongName} on the ArgumentsModel, "
                    + $"must be a boolean for {nameof(OptionAttribute)}. Use {nameof(OptionParameterAttribute)} instead");
            }

            return value;
        }
    }
}
