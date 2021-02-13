using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CustomException
{
   public class AuthorizationException:Exception
    {
        public AuthorizationException(string errorMessage):base(errorMessage)
        {
            
        }
    }
}
