using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
   public class AccountNotFoundException :  DataNotFoundException
    {
        public AccountNotFoundException()
        {

        }
        public AccountNotFoundException(Guid accountId) : base($"Account with id:{accountId} was not found.")
        {

        }
    }
}
