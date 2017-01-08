### Dependencies

* dotnet cli - https://www.microsoft.com/net/core
* doxfx cli - https://dotnet.github.io/docfx/

### Brief

There are two phases to the documentation build.

1. Generate the custom documentation: see `./Website` which involved building and running the .Net project there. It outputs docs into the necessary docfx folders
2. Generate the docfx site: `./docfx_project` -> `docfx build`

The first stage is added because building the markdown from a real codebase allows for compile time checks on the example code, which makes writing accurate documentation easier.