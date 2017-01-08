#define CODE

using System;
using System.Linq;

using EntryPoint;
using System.Collections.Generic;

namespace Website {
    class article_intro {
        /// Entrypoint is an argument parser designed to be composable, practical to use, and maintainable over the life time of a project.
        /// 
        /// It's based around the concept of Declarative POCOs (CliArguments, and CliCommands classes), which you simply pass to EntryPoint for parsing and construction.
        /// 
        /// Parses arguments in the form `UtilityName [command] [-o | --options] [operands]`
        ///
        /// ### Standards
        /// 
        /// * .Net Standard 1.6+ (All future .Net releases are built on this)
        /// * .Net Framework 4.5+
        ///
        /// Follows the [IEEE Standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) for command line utilities 
        /// closely, but does include common adblibs, such as fully named `--option` style options.
        ///
        /// ### Installation
        /// EntryPoint is available on [NuGet](https://www.nuget.org/packages/EntryPoint):
        /// 
        ///     PM> Install-Package EntryPoint
        ///     
    }

    /// ### Introduction
    /// There are just a few classes you'll interact with:
    /// 
    /// * `Cli` - The main API, which handles all processing
    /// * `BaseCliArguments` - An abstract class which you implement to define Options & Operands, known as CliArguments classes
    /// * `BaseCliCommands` - An abstract class which you implement to define Commands, known as CliCommands classes
    /// * `Attributes` - There are a handful of attributes you can use to define your CliCommands and CliArguments implementations, which are documented in depth below
}