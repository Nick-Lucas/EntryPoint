using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {

    /// <summary>
    /// Used to mark a method as a Command, in a CliCommands class
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class CommandAttribute : Attribute {

        /// <summary>
        /// Marks a Method as a Command
        /// </summary>
        /// <param name="Name">The case in-sensitive name for the command, which is invoked to activate it</param>
        public CommandAttribute(string Name) {
            if (Name == null || Name.Length == 0) {
                throw new ArgumentException(
                    $"A {nameof(CommandAttribute)} was not given a name");
            }
            this.Name = Name;
        }

        /// <summary>
        /// The case in-sensitive name for the command
        /// </summary>
        internal string Name { get; set; }
    }

}
