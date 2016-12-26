using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.OptionStrategies {
    interface IValueDefaulter {
        DefaultValueBehaviourEnum DefaultValueBehaviour { get; set; }
        object CustomDefaultValue { get; set; }
    }
}
