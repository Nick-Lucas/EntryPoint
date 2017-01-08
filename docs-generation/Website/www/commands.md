## Commands

Although it's perfectly fine to only use a CliArguments class for a simple application,
if you have multiple Commands, each with a different set of Arguments; you may want
to create multiple application entry points, each with its own CliArguments class.

This is the purpose of `BaseCliCommands`.

#### Basic Usage

    class SimpleCommandsProgram {
        public void Main(string[] args) {
            // One line execution of the Commands class
            // It will select and route to one of your Command methods
            var commands = Cli.Execute<SimpleCliCommands>(args);
        }
    }

    class SimpleCliCommands : BaseCliCommands {

        // A command is a Method which takes a `string[]`.
        // You also need to apply a [Command(name)] attribute, 
        // with the name of the command on the CLI
        [Command("command1")]
        public void Command1(string[] args) {
            // var arguments = Cli.Parse<Command1CliArguments>(args);
            // ...Application logic
        }

        // You can also define a Default command.
        // This helps if you want a fallback when the user doesn't name a command
        [DefaultCommand]
        [Command("command2")]
        public void Command2(string[] args) {
            // var arguments = Cli.Parse<Command2CliArguments>(args);
            // ...Application logic
        }
    }

#### Attributes

There are several attributes which can be applied to a CliCommands class

##### `[Command(Name = string)]`
* **Apply to:** Methods with the signature: `void MethodName(string[])`
* **Argument, Name:** This is the Command Name to be used on the CLI like: `Utility [Command Name] [options]`
* **Detail:** Defines a method as a Command to be routed to

##### `[DefaultCommand]`
* **Apply to:** Command methods
* **Detail:** Defines a Command as the default when no Command is specified, otherwise EntryPoint invokes `--help`

##### `[Help(detail = string)]`
* **Apply to:** CliCommands classes and Command methods
* **Detail:** Provides custom documentation on a Command