[![Build status](https://ci.appveyor.com/api/projects/status/bocpkn9t5lhan1o9?svg=true)](https://ci.appveyor.com/project/Nick-Lucas/entrypoint)
[![NuGet](https://img.shields.io/nuget/v/EntryPoint.svg)](https://www.nuget.org/packages/EntryPoint)
[![MIT License](https://img.shields.io/github/license/Nick-Lucas/EntryPoint.svg)](https://github.com/Nick-Lucas/EntryPoint/blob/master/LICENSE)

## EntryPoint

Composable CLI Argument Parser for all modern .Net platforms

Parses arguments in the form `UtilityName [command] [-o | --options] [operands]`

Supports:

* .Net Standard 1.6+ (.Net Core and all future .Net releases are built on this)
* .Net Framework 4.5+

Follows the [IEEE Standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) closely, but does include common adblibs such as fully named `--option` style options.

## Installation
EntryPoint is available on [NuGet](https://www.nuget.org/packages/EntryPoint):

	PM> Install-Package EntryPoint

Pull requests and suggestions are welcome, and some small tasks are already in the Issues.

## Documentation

* Full documentation: https://nick-lucas.github.io/EntryPoint/

* Example Implementation: https://github.com/Nick-Lucas/EntryPoint/tree/master/test/Example

## As simple as...

Parse your application's Command Line Arguments into a declarative POCO, in one line.

Arguments are defined as declarative POCOs using Attributes.
```C#
var arguments = Cli.Parse<CliArguments>(args);
if (arguments.Option) {
  // ...
};
```
```C#
public class CliArguments : BaseCliArguments {
    public CliArguments() : base("MyApplication") { }

    [Option(ShortName: 'o',
            LongName: "option-1")]
    public bool Option { get; set; }
}
```

Commands have a dedicated API:
```C#
Cli.Execute<CliCommands>(args);
```
```C#
public class CliCommands : BaseCliCommands {
    [Command("primary")]
    public void Primary(string[] args) {
    	// Arguments Parsing and Command Code...
    }
}
```
