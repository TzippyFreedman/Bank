using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
   public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
        {

        }
        public AccountNotFoundException(Guid accountId) : base($"Account id:{accountId} was not found")
        {

        }
    }
}
