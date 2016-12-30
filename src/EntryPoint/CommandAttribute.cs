using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint {
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute {
        public CommandAttribute(string Name) {
            this.Name = Name;
        }

        internal string Name { get; set; }
    }
}
