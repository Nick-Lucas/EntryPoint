using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;

namespace EntryPoint {

    /// <summary>
    /// The main API for EntryPoint
    /// </summary>
    public static class EntryPointApi {
        internal const string DASH_SINGLE = "-";
        internal const string DASH_DOUBLE = "--";

        /// <summary>
        /// Create and populate a custom ArgumentsModel from the Environment arguments
        /// </summary>
        /// <typeparam name="A">The type of the ArgumentsModel, which derives from BaseArgumentsModel</typeparam>
        /// <returns>A populated ArgumentsModel</returns>
        public static A Parse<A>() where A : BaseArgumentsModel, new() {
            var args = Environment.GetCommandLineArgs();
            return Parse(new A(), args);
        }

        /// <summary>
        /// Create and populate a custom ArgumentsModel
        /// </summary>
        /// <typeparam name="A">The type of the ArgumentsModel, which derives from BaseArgumentsModel</typeparam>
        /// <param name="args">The CLI argruments input</param>
        /// <returns>A populated ArgumentsModel</returns>
        public static A Parse<A>(string[] args) where A : BaseArgumentsModel, new() {
            return Parse(new A(), args);
        }

        /// <summary>
        /// Populate a given custom ArgumentsModel
        /// </summary>
        /// <typeparam name="A">The type of the ArgumentsModel, which derives from BaseArgumentsModel</typeparam>
        /// <param name="argmentsModel">The pre-instantiated ArgumentsModel</param>
        /// <param name="args">The CLI argruments input</param>
        /// <returns>A populated ArgumentsModel</returns>
        public static A Parse<A>(A argmentsModel, string[] args) where A : BaseArgumentsModel {
            string arguments = string.Join(" ", args);
            var tokens = Tokeniser.MakeTokens(arguments);
            return Parser.ParseAttributes(argmentsModel, tokens);
        }

    }

}
