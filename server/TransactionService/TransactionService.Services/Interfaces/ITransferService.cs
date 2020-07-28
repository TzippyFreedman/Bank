using System.Threading.Tasks;
using TransactionService.Contract.Models;

namespace TransactionService.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionModel> AddAsync(TransactionModel newTransaction);
    }
}
