* `Cli.Parse` and `Cli.Execute` have several overloads available. They can create the class and get the command line arguments themselves, but give you manual control, too.
* Short named options `-o` are case sensitive: `-a != -A`
* Long named options `--option` are case insensitive: `--opt == --Opt`
* Options can be combined by the user: `-a -b -c` -> `-abc`
* Combined options can end with an option parameter: `-abco value`
* Option-parameters have several forms: `-o value` `-o=value` `--option value` `--option=value`
* Quotes and Escape characters are both supported: `--option "my value"` `--option \-my-value`
* **Warning:** be careful with Quotes as .Net respects and then removes them during `string[] args` creation. They can be escaped to include in values.