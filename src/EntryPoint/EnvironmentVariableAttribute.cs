using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Arguments.OptionStrategies;

namespace EntryPoint {

    /// <summary>
    /// Declares an Environment Variable
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class EnvironmentVariableAttribute : Attribute {

        /// <summary>
        /// Declares an Environment Variable
        /// </summary>
        /// <param name="VariableName">The name of the Environment Variable</param>
        public EnvironmentVariableAttribute(string VariableName) {
            this.VariableName = VariableName;
        }

        /// <summary>
        /// The name of the Environment Variable
        /// </summary>
        public string VariableName { get; set; }

    }

}
