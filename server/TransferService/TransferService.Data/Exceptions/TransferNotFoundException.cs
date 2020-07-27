using System;
using System.Collections.Generic;
using System.Text;

namespace TransferService.Data.Exceptions
{
  public  class TransferNotFoundException: Exception
    {
        public TransferNotFoundException()
        {

        }
        public TransferNotFoundException(Guid transferId) : base($"transfer with id:{transferId} was not found.")
        {

        }
        public
    }
}
