using EntryPoint.ArgumentTypeParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Helpers {
    [AttributeUsage(
        validOn: AttributeTargets.Field | AttributeTargets.Property, 
        AllowMultiple = true, 
        Inherited = true)]
    public class BaseArgumentAttribute : Attribute {
        internal BaseArgumentAttribute(IArgumentType argumentType) {
            ArgumentType = argumentType;
        }

        internal IArgumentType ArgumentType { get; private set; }

        public object DefaultValue { get; set; } = null;

        public char SingleDashChar { get; set; }
        internal int SingleDashIndex(string[] args) {
            if (SingleDashChar == char.MinValue) {
                return -1;
            }

            return args.SingleDashIndex(SingleDashChar);
        }

        public string DoubleDashName { get; set; }
        internal int DoubleDashIndex(string[] args) {
            if (DoubleDashName == string.Empty) {
                return -1;
            }

            return args.DoubleDashIndex(DoubleDashName);
        }
    }
}
