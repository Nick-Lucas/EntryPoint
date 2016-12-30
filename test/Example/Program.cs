using System;
using System.Linq;

using EntryPoint;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            EntryPointApi.ExecuteCommand<Commands>(args);
        }
    }
}
