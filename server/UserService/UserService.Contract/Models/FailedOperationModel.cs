namespace UserService.Contract.Models
{
    public class FailedOperationModel : OperationModel
    {
        public string FailureReason { get; set; }
    }
}
