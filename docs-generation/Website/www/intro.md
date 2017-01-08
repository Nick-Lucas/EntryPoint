## Introduction

Entrypoint is an argument parser designed to be composable, practical to use, and maintainable over the life time of a project.

It's based around the concept of Declarative POCOs (`CliArguments`, and `CliCommands` classes), which you simply pass to EntryPoint for parsing and construction.

Entrypoint parses arguments in the form: `UtilityName [command] [-o | --options] [operands]`

#### Standards

* .Net Standard 1.6+ (All future .Net releases are built on this)
* .Net Framework 4.5+

Follows the [IEEE Standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) for command line utilities
closely, but does include common adblibs, such as fully named `--option` style options.

#### Installation
EntryPoint is available on [NuGet](https://www.nuget.org/packages/EntryPoint):

    PM> Install-Package EntryPoint
