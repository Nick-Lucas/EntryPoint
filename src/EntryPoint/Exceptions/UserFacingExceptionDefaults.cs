using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    internal static class UserFacingExceptionDefaults {
        public static void OnUserFacingException(UserFacingException e, string message) {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Arguments Error: ");
            Console.WriteLine(message);
            Console.ResetColor();

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
