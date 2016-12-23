A simple CLI argument parser for all modern .Net platforms

# TODO

---
* Look at behaviour of .Net parsing into array elements, to educate tests
	* Test .Net parsing of quoted sections - do they end up in the same array element?
* Follow IEEE standard: http://pubs.opengroup.org/onlinepubs/9699919799/basedefs/V1_chap12.html

---
* Support combined switches `-a -b -c` `-abc`, but reject combinations for parameters
* Produce --help documentation automatically, and allow user addition to this. Add DocumentationAttribute as an option for the user
* Add REQUIRED bool to attribute
* Validate the arguments model for duplicate options and other issues

---
* API Documentation


# Behaviour

* -O options are case sensitive
* --option-name options are case insensitive