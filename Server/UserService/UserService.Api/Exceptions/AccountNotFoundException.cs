using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Api.Exceptions
{
    class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
        {

        }
        public AccountNotFoundException(Guid accountId) : base($"Account id:{accountId} was not found")
        {

        }
    }
}
