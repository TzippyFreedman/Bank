using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
   public class VerificationNotFoundException: DataNotFoundException
    {
        public VerificationNotFoundException()
        {

        }
        public VerificationNotFoundException(string email) : base($"Verification for email :{email} was not found.")
        {

        }
    }
}
