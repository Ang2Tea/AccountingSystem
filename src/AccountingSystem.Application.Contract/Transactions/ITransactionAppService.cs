using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountingSystem.Application.Contract.Transactions
{
    public interface ITransactionAppService
    {
        Task<List<TransactionDto>> GetListAsync();
        Task AddTransaction(CreateTransactionInput transaction);
    }
}