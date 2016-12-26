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

        // ** Parsing **

        /// <summary>
        /// Create and populate an ApplicationOptions implementation from the command line args
        /// </summary>
        /// <typeparam name="A">The type of the ApplicationOptions, which derives from BaseArgumentsModel</typeparam>
        /// <returns>A populated ApplicationOptions instance</returns>
        public static A Parse<A>() where A : BaseApplicationOptions, new() {
            var args = Environment.GetCommandLineArgs();
            return Parse(new A(), args);
        }

        /// <summary>
        /// Create and populate an ApplicationOptions implementation
        /// </summary>
        /// <typeparam name="A">The type of the ApplicationOptions, which derives from BaseArgumentsModel</typeparam>
        /// <param name="args">The CLI argruments input</param>
        /// <returns>A populated ApplicationOptions instance</returns>
        public static A Parse<A>(string[] args) where A : BaseApplicationOptions, new() {
            return Parse(new A(), args);
        }

        /// <summary>
        /// Populate a given ApplicationOptions instance
        /// </summary>
        /// <typeparam name="A">The type of the ApplicationOptions, which derives from BaseArgumentsModel</typeparam>
        /// <param name="applicationOptions">The pre-instantiated ApplicationOptions implementation</param>
        /// <param name="args">The CLI argruments input</param>
        /// <returns>A populated ApplicationOptions instance</returns>
        public static A Parse<A>(A applicationOptions, string[] args) where A : BaseApplicationOptions {

            // Process inputs
            Model model = new Model(applicationOptions);
            var tokens = Tokeniser.MakeTokens(args);
            ParseResult parseResult = Parser.MakeParseResult(tokens, model);
            
            // Map results
            model = Mapper.MapOptions(model, parseResult);
            
            // If --help was requested
            if (model.ApplicationOptions.HelpRequested) {
                Help.HandleHelpRequest(model);
            }

            return (A)model.ApplicationOptions;
        }


        // ** Help **

        /// <summary>
        /// Generate and return a Help string for a given BaseApplicationOptions instance
        /// </summary>
        /// <typeparam name="A">Custom implementation type of BaseApplicationOptions which can be created with 0 arguments</typeparam>
        /// <returns>Help string</returns>
        public static string GenerateHelp<A>() where A : BaseApplicationOptions, new() {
            return GenerateHelp(new A());
        }

        /// <summary>
        /// Generate and return a Help string for a given BaseApplicationOptions instance
        /// </summary>
        /// <typeparam name="A">Custom implementation type of BaseApplicationOptions</typeparam>
        /// <param name="applicationOptions">Instance of custom BaseApplicationOptions implementation</param>
        /// <returns>Help string</returns>
        public static string GenerateHelp<A>(A applicationOptions) where A : BaseApplicationOptions, new() {
            return Help.Generate(new Model(applicationOptions));
        }
    }

}
