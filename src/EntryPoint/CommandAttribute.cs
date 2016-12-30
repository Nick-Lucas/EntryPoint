using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class CommandAttribute : Attribute {
        public CommandAttribute(string Name) {
            if (Name == null || Name.Length == 0) {
                throw new ArgumentException(
                    "You must give a Command a name");
            }
            this.Name = Name;
        }

        internal string Name { get; set; }
    }
}
