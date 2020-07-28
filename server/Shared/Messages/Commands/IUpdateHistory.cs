using System;

namespace Messages.Commands
{
    public interface IUpdateHistory
    {
        public Guid SrcAccountId { get; set; }
        public Guid DestAccountId { get; set; }
        public Guid TransactionId { get; set; }
        public int TransactionAmount { get; set; }
        public int SrcBalance { get; set; }
        public int DestBalance { get; set; }
        public DateTime OperationTime { get; set; }

    }
}
