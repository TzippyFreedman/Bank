using System;

namespace TransactionService.Data.Exceptions
{
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException()
        {

        }
        public TransactionNotFoundException(Guid transactionId) : base($"transaction with id:{transactionId} was not found.")
        {

        }
    }
}
