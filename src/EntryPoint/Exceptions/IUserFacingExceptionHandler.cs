using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    public interface IUserFacingExceptionHandler {

        /// <summary>
        /// Set True whenever a UserFacingException bubbles up and is handled by the instance implementing this interface
        /// </summary>
        bool UserFacingExceptionThrown { get; set; }

        /// <summary>
        /// Called whenever a UserFacingException bubbles up and is handled by the instance implementing this interface
        /// </summary>
        /// <param name="e">thrown exception</param>
        /// <param name="message">a message to display to the user</param>
        void OnUserFacingException(UserFacingException e, string message);

    }
}
