using System;
using AccountingSystem.Application.Contract.ShopItems;

namespace AccountingSystem.Application.Contract.Transactions
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        
        public ShopItemDto ShopItem { get; set; }
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
    }
}