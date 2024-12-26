using System;

namespace AccountingSystem.Application.Contract.Transactions
{
    public class CreateTransactionInput
    {
        public Guid ShopItemId { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
    }
}