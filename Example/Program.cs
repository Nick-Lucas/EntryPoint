using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EntryPoint;

namespace Example {
    public class Program {
        public static void Main(string[] args) {
            MyArgs a = EntryPointApi.Parse<MyArgs>(args);
        }
    }
}
