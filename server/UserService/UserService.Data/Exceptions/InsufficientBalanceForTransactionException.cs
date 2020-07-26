using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
    class InsufficientBalanceForTransactionException : Exception
    {
        public InsufficientBalanceForTransactionException()
        {

        }
        public InsufficientBalanceForTransactionException(Guid accountId, int amount) : base($"Insufficient Balance For Transaction from acccount :{accountId} for amount:{amount}.")
        {

        }
    }
}
