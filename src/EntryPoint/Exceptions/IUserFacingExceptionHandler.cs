using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntryPoint.Exceptions {
    interface IUserFacingExceptionHandler {
        bool UserFacingExceptionThrown { get; set; }
        void OnUserFacingException(UserFacingException e, string message);
    }
}
