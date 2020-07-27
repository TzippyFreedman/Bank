namespace Messages.Messages
{
    public interface ICommitTransferResponse
    {
        public bool IsTransferSucceeded { get; set; }
        public string FailureReason { get; set; }


    }
}
