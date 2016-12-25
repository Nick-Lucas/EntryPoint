using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint.Internals;
using EntryPoint.Parsing;
using EntryPoint.OptionModel;

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
        public static A Parse<A>() where A : BaseApplicationOptions, new() {
            var args = Environment.GetCommandLineArgs();
            return Parse(new A(), args);
        }

        /// <summary>
        /// Create and populate a custom ArgumentsModel
        /// </summary>
        /// <typeparam name="A">The type of the ArgumentsModel, which derives from BaseArgumentsModel</typeparam>
        /// <param name="args">The CLI argruments input</param>
        /// <returns>A populated ArgumentsModel</returns>
        public static A Parse<A>(string[] args) where A : BaseApplicationOptions, new() {
            return Parse(new A(), args);
        }

        /// <summary>
        /// Populate a given custom ArgumentsModel
        /// </summary>
        /// <typeparam name="A">The type of the ArgumentsModel, which derives from BaseArgumentsModel</typeparam>
        /// <param name="applicationOptions">The pre-instantiated ArgumentsModel</param>
        /// <param name="args">The CLI argruments input</param>
        /// <returns>A populated ArgumentsModel</returns>
        public static A Parse<A>(A applicationOptions, string[] args) where A : BaseApplicationOptions {
            string arguments = string.Join(" ", args);

            // Process inputs
            Model model = new Model(applicationOptions);
            var tokens = Tokeniser.MakeTokens(arguments);
            ParseResult parseResult = Parser.MakeParseResult(tokens, model);
            
            // Map results
            model = Mapper.MapOptions(model, parseResult);
            
            // If --help was requested
            if (model.ApplicationOptions.HelpRequested) {
                Help.HandleHelpRequest(model);
            }

            return (A)model.ApplicationOptions;
        }
    }

}
