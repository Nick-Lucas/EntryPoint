## Arguments

For a simple application you may not need Commands; `CliArguments` classes are used
to parse command line arguments without consideration of Commands.

#### Example

Let's say we want a utility used like: `UtilityName [-s] [--name Bob] [6.1]`

This has one Option `-s`, one OptionParameter `--name Bob` and a positional Operand `6.1`

    class SimpleProgram {
        void main(string[] args) {

            // One line parsing of any `BaseCliArguments` implementation
            var arguments = Cli.Parse<SimpleCliArguments>(args);

            // Object oriented access to your arguments
            Console.WriteLine("The name is: " + arguments.Name);
            Console.WriteLine("Switch flag: " + arguments.Switch);
            Console.WriteLine("Positional Operand 1: " + arguments.FirstOperand);
        }
    }

    class SimpleCliArguments : BaseCliArguments {
        public SimpleCliArguments() : base("SimpleApp") { }

        // Defining a CliArguments class is as easy as adding 
        // attributes to your POCO properties
        [Option(LongName: "switch",
                ShortName: 's')]
        public bool Switch { get; set; }

        // Both Option and OptionParameter attributes support a 
        // combination of -o and --option style invocations
        [OptionParameter(LongName: "name",
                         ShortName: 'n')]
        public string Name { get; set; }

        // Operands can be mapped positionally
        // But BaseCliArguments also has a .Operands string[] where 
        // un-mapped operands are stored
        [Operand(Position: 1)]
        public decimal FirstOperand { get; set; }

        // When the user makes a mistake, a default handler takes over
        // You can override this behaviour on this Virtual method
        public override void OnUserFacingException(UserFacingException e, string message) {
            throw new NotImplementedException();
        }

        // When the user invokes --help/-h a default handler will take over
        // You can define your own behaviour by overriding this Virtual method
        public override void OnHelpInvoked(string helpText) {
            throw new NotImplementedException();
        }
    }

#### Attributes

We use Attributes to define CLI functionality

##### `[Option(LongName = string, ShortName = char)]`
* **Apply to:** Class Properties
* **Output Types:** Bool
* **Detail:** Defines an On/Off option for use on the CLI
* **Argument, LongName:** the case in-sensitive name to be used like `--name`
* **Argument, ShortName:** the case sensitive character to be used like `-n`
* At least one name needs to be provided

##### `[OptionParameter(LongName = string, ShortName = char)]`
* **Apply to:** Class Properties
* **Output Types:** Primitive Types, Enums, Lists of Primitives or Enums
* **Detail:** Defines a parameter which can be invoked to provide a value
* **Argument, LongName:** the case in-sensitive name to be used like `--name`
* **Argument, ShortName:** the case sensitive character to be used like `-n`
* **Also Supports:** `Required` and `Help` attributes
* At least one name needs to be provided

##### `[Operand(Position = int)]`
* **Apply to:** Class Properties
* **Output Types:** Primitive Types, Enums, Lists of Primitives or Enums
* **Detail:** Maps a positional operand from the end of a CLI command
* **Argument, Position:** the 1 based position of the Operand
* **Also Supports:** `Required` and `Help` attributes

##### `[EnvironmentVariable(VariableName = string)]`
* **Apply to:** Class Properties
* **Output Types:** Primitive Types, Enums, Lists of Primitives or Enums
* **Detail:** Maps a variable from the environment
* **Argument, VariableName:** the case sensitive name of the environment variable
* **Also Supports:** `Required` and `Help` attributes
* Be aware: VariableName case-sensitivity is platform dependent. Linux will be case-sensitive and Windows is not

##### `[Required]`
* **Apply to:** Option, OptionParameter or Operand properties
* **Detail:** Makes an Option or Operand mandatory for the user to provide

##### `[Help(Detail = string)]`
* **Apply to:** Class Properties with any Option or Operand Attribute applied, or an CliArguments Class
* **Detail:** Provides custom documentation on an Option, Operand or CliArguments Class, which will be consumed by the help generator

#### Use case

The following is an example implementation for use in a simple message sending application

The following is used like:

`UtilityName [ -v | --verbose ] [ -s | --subject "your subject" ] [ -i | --importance ] [ normal | high ] ] [message]`

    // Usage is as simple as
    class MessagingProgram {
        void main(string[] args) {
            var arguments = Cli.Parse<MessagingCliArguments>(args);

            // Use the arguments object...
        }
    }

    class MessagingCliArguments : BaseCliArguments {
        public MessagingCliArguments() : base("Message Sender") { }

        // Verbose will be a familiar option to most CLI users
        [Option(LongName: "verbose",
                ShortName: 'v')]
        public bool Verbose { get; set; }

        // A subject *must* be provided by the user 
        [Required]
        [OptionParameter(LongName: "subject",
                         ShortName: 's')]
        public string Subject { get; set; }

        // An enum importance level for the message.
        // If not provided this is defaulted to `Normal`
        // User can provide the value as a number or string (ie. '2' or 'high')
        [OptionParameter(LongName: "importance",
                         ShortName: 'i')]
        public MessageImportanceEnum Importance { get; set; } = MessageImportanceEnum.Normal;

        // A list of strings
        // Lists support all the same types as any other option parameter
        // The Cli expects list values in the form `item1,item2,item3` etc
        [OptionParameter(LongName: "recipients")]
        public List<string> Recipients { get; set; }

        // Variables can be mapped from the environment
        // Be aware that case-sensitivity of VariableName is dependent on platform.
        //  - Linux is case-sensitive
        //  - Windows is case-insensitive
        [Required]
        [EnvironmentVariable("MSGSDR_PASSWORD")]
        public string Password { get; set; }

        // A message *must* be provided as the first operand
        [Required]
        [Operand(1)]
        public string Message { get; set; }
    }

    enum MessageImportanceEnum {
        Normal = 1,
        High = 2
    }

#### Value Defaults

If the user does not provide an non-required option-parameter or operand,
it can be useful to configure the application with a default.

This is easily done using C# property initialisers,
and will otherwise use the type's default value


    // The following Importance Enum will always be set to 'Normal'
    // if the user does not provide a value
    [OptionParameter(LongName: "importance",
                     ShortName: 'i')]
    public MessageImportanceEnum Importance { get; set; } = MessageImportanceEnum.Normal;


#### Supported Types

Both OptionParameter and Operand arguments can be mapped to a number of different .Net types

* **Primitive & 'Primitive like' Types**
    * These are your String, Int, Long, Double, Float, Decimal, Bool, etc...
    * Should support any simple type which implements `IConvertible`, although this can't be exhaustively tested

* **Enums**
    * Parses custom enums from both the numeric value or the string/name for a value

* **Lists**
    * Supports the generic collection: `List<T>`
    * Parses lists from the form `item1,item2,item3`
    * `T` can be any type supported *above*