namespace Messages.Messages
{
    public interface ICommitTransactionResponse
    {
        public bool IsTransactionSucceeded { get; set; }
        public string FailureReason { get; set; }


    }
}
