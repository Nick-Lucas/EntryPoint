[![Build status](https://ci.appveyor.com/api/projects/status/bocpkn9t5lhan1o9?svg=true)](https://ci.appveyor.com/project/Nick-Lucas/entrypoint)
[![NuGet](https://img.shields.io/nuget/v/EntryPoint.svg)](https://www.nuget.org/packages/EntryPoint)
[![MIT License](https://img.shields.io/github/license/Nick-Lucas/EntryPoint.svg)](https://github.com/Nick-Lucas/EntryPoint/blob/master/LICENSE)

**Warning: (Version 0.9.6)** EntryPoint is approaching v1.0 but changes to the API may yet come. See the roadmap below for more information

## EntryPoint

Lightweight and Composable CLI Argument Parser for all modern .Net platforms

Parses arguments in the form `UtilityName[-o | --options][operands]`

Supports:

* .Net Standard 1.6+ (All future.Net releases are built on this)
* .Net Framework 4.5+

Follows the [IEEE Standard](http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html) closely, but does include common adblibs such as fully named `--option` style options.

## Installation
EntryPoint is available on [NuGet](https://www.nuget.org/packages/EntryPoint):

	PM> Install-Package EntryPoint

Pull requests and suggestions are welcome, and some small tasks are already in the Issues.

## As simple as...

Parse your application's Command Line Arguments into a declarative POCO, in one line
```C#
var options = EntryPointApi.Parse<MyApplicationOptions>(args);
var someOption = options.SomeOption;
```

Full documentation: https://nick-lucas.github.io/EntryPoint/

## Roadmap
* **Future**
	* Support Lists
	* Support Data Validation
	* 1.0 Release