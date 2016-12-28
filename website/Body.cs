using System;
using System.Linq;

using EntryPoint;

namespace Website {
    class Body {
        /// ## About EntryPoint
        /// 
        /// An argument parser designed to be composable, practical and maintainable.
        /// 
        /// Parses arguments in the form `UtilityName [-o | --options] [operands]`
        ///
        /// Supports:
        /// 
        /// * .Net Standard 1.6+ (All future .Net releases are built on this)
        /// * .Net Framework 4.5+
        ///
        /// Follows the [IEEE Standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) 
        /// closely, but does include common adblibs such as fully named `--option` style options.
        ///
        /// ## Installation
        /// EntryPoint is available on [NuGet](https://www.nuget.org/packages/EntryPoint):
        /// 
        ///     PM> Install-Package EntryPoint
        ///     
    }

    /// ## Introduction
    /// EntryPoint is simple to use, and composable. It has a few tools you'll use:
    /// 
    /// * `EntryPointApi` - The main API, which handles all processing
    /// * `BaseApplicationOptions` - An abstract class which you implement to define your CLI Options & Operands
    /// * `Attributes` - There are a small handful of attributes you can use to define your ApplicationOptions implementation, documented below.
    /// 

    /// ## Basic Usage
    /// Everything revolves around declarative `ApplicationOptions` 
    /// classes which EntryPoint uses to parse command line arguments
    /// 
    /// Let's say we want a utility used like: `UtilityName [-s] [--name Bob] [6.1]`
    /// 
    /// This has one Option, one OptionParameter and a positional Operand
#if CODE
    class SimpleApplicationOptions : BaseApplicationOptions {
        public SimpleApplicationOptions() : base("SimpleApp") { }

        // Option
        [Option(LongName = "switch", 
                ShortName = 's')]
        public bool Switch { get; set; }

        // Option-Parameter
        [OptionParameter(LongName = "name", 
                         ShortName = 'n')]
        public string Name { get; set; }

        // Operand
        [Operand(position: 1)]
        public decimal FirstOperand { get; set; }
    }

    class SimpleProgram {
        void main(string[] args) {
            
            // One line parsing of any `BaseApplicationOptions` implementation
            var options = EntryPointApi.Parse<SimpleApplicationOptions>(args);

            // Object oriented access to your arguments
            Console.WriteLine($"The name is {options.Name}");
            Console.WriteLine($"Switch flag: {options.Switch}");
            Console.WriteLine($"Positional Operand 1: {options.FirstOperand}");
        }
    }
#endif

    /// ## Attributes
    /// 
    /// `BaseApplicationOptions` implementations use Attributes to define CLI functionality
    /// 
    /// #### `[Option(LongName = string, ShortName = char)]`
    /// * **Apply to:** Class Properties
    /// * **Output Types:** Bool
    /// * **Detail:** Defines an On/Off option for use on the CLI
    /// * **Argument, LongName:** the case in-sensitive name to be used like `--name`
    /// * **Argument, ShortName:** the case sensitive character to be used like `-n`
    /// * At least one name needs to be provided
    /// 
    /// #### `[OptionParameter(LongName = string, ShortName = char)]`
    /// * **Apply to:** Class Properties
    /// * **Output Types:** Primitive Types, Enums
    /// * **Detail:** Defines a parameter which can be invoked to provide a value
    /// * **Argument, LongName:** the case in-sensitive name to be used like `--name`
    /// * **Argument, ShortName:** the case sensitive character to be used like `-n`
    /// * At least one name needs to be provided
    /// 
    /// #### `[Operand(position = int)]`
    /// * **Apply to:** Class Properties
    /// * **Output Types:** Primitive Types, Enums
    /// * **Detail:** Maps a positional operand from the end of a CLI command
    /// * **Argument, Position:** the 1 based position of the Operand
    /// 
    /// #### `[Required]`
    /// * **Apply to:** Class Properties with any Option or Operand Attribute applied
    /// * **Detail:** Makes an Option or Operand mandatory for the user to provide
    /// 
    /// #### `[Help(detail = string)]`
    /// * **Apply to:** Class Properties with any Option or Operand Attribute applied, or an ApplicationOptions Class
    /// * **Detail:** Provides custom documentation on an Option, Operand or ApplicationOptions Class, which will be consumed by the help generator
    ///


    /// ## Example Application
    /// 
    /// The following is an example implementation for use in a simple message sending application
    /// 
    /// This is used like `UtilityName [ -v | --verbose ] [ -s | --subject "your subject" ] [ -i | --importance [ 1 | normal | 2 | high ] ] [message]`
#if CODE
    class MessagingApplicationOptions : BaseApplicationOptions {
        public MessagingApplicationOptions() : base("Message Sender") { }

        // Verbose will be a familiar option to most CLI users
        [Option(LongName = "verbose", 
                ShortName = 'v')]
        [Help("When this is set, verbose logging will be activated")]
        public bool Verbose { get; set; }

        // A subject *must* be provided by the user 
        [Required]
        [OptionParameter(LongName = "subject",
                         ShortName = 's')]
        [Help("Mandatory Subject to provide")]
        public string Subject { get; set; }

        // An importance level for the message.
        // If not provided this is defaulted to `Normal`
        // User can provide the value as a number or string (ie. '2' or 'high')
        [OptionParameter(LongName = "importance", 
                         ShortName = 'i')]
        [Help("Sets the importance level of a sent message")]
        public MessageImportanceEnum Importance { get; set; } = MessageImportanceEnum.Normal;

        // A message *must* be provided as the first operand
        [Required]
        [Operand(1)]
        [Help("Mandatory message to provide")]
        public string Message { get; set; }
    }

    // Usage is then as simple as
    class MessagingProgram {
        void main(string[] args) {
            var options = EntryPointApi.Parse<MessagingApplicationOptions>(args);

            // Use the options object...
        }
    }

    enum MessageImportanceEnum {
        Normal = 1,
        High = 2
    }
#endif

    /// ## Value Defaults
    /// 
    /// If the user does not provide an non-required option-parameter or operand, 
    /// it can be useful to configure the application with a default.
    /// 
    /// This is easily done using C# property initialisers, 
    /// and will otherwise use the type's default value
    /// 
    class DefaultsExample {
#if CODE
        // The following Importance Enum will always be set to 'Normal'
        // if the user does not provide a value
        [OptionParameter(LongName = "importance",
                         ShortName = 'i')]
        [Help("Sets the importance level of a sent message")]
        public MessageImportanceEnum Importance { get; set; } = MessageImportanceEnum.Normal;
#endif
    }

    /// ## Help Generator
    /// 
    /// EntryPoint provides an automatic Help generator, which always owns the `-h` and `--help` Options.
    /// 
    /// This sets the `BaseApplicationOptions.HelpRequested` property; every ApplicationOptions class therefore has it.
    /// 
    /// It consumes the following information:
#if CODE
    [Help("This will be displayed as an initial blurb for the utility")]
    class HelpApplicationOptions : BaseApplicationOptions {
        public HelpApplicationOptions() 
            : base(utilityName: "Displayed as the application name") { }

        // [Option(...)]
        // [OptionParameter(...)]
        // [Operand(...)]
        [Help("Displayed as additional instructions for an Option/Operand")]
        public bool Value { get; set; }
    }
#endif

    /// A simple implementation would therefore look like this:
#if CODE
    class HelpProgram {
        public void main(string[] args) {
            var options = EntryPointApi.Parse<HelpApplicationOptions>(args);
            if (options.HelpRequested) {
                string help = EntryPointApi.GenerateHelp<HelpApplicationOptions>();
                Console.WriteLine(help);
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
                return;
            }

            // Application code...
        }
    }
#endif
    /// It is recommended that you always check for `.HelpRequested`, as invoking `--help`
    /// will disable exceptions for all `[Required]` Options or Operands. 
    /// Help is expected to take precedence over other options

    /// ## Tips & Behaviour
    /// 
    /// * `EntryPointApi.Parse` has several overloads available. It can create the class and get the command line arguments itself, but gives you manual control, too.
    /// * Short named options `-o` are case sensitive: `-a != -A`
    /// * Long named options `--option` are case insensitive: `--opt == --Opt`
    /// * Options can be combined by the user: `-a -b -c` -> `-abc`
    /// * Combined options can end with an option-parameter: `-abco value`
    /// * Option-parameters have several forms: `-o value` `-o=value` `--option value` `--option=value`
    /// * Quotes and Escape characters are both supported: `--option "my value"` `--option \-my-value`
    /// * **Warning:** be careful with Quotes as .Net respects and then removes them during `string[] args` creation. They can be escaped to include in values.
}