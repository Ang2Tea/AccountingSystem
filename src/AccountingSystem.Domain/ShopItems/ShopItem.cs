using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.Transactions;

namespace AccountingSystem.Domain.ShopItems
{
    public class ShopItem : Entity<Guid>
    {
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public List<TransactionShopItem> Transactions { get; private set; }
        
        public ShopItem() {}
        public ShopItem(Guid id) : base(id) {}

        public ShopItem(Category category, string name, string description, decimal price)
        {
            CategoryId = category.Id;    
            Category = category;
            Name = name;
            Description = description;
            Price = price;
            Transactions = new List<TransactionShopItem>();
        }

        public Task AddTransaction(TransactionType transactionType, int amount)
        {
            if (Transactions == null)
                Transactions = new List<TransactionShopItem>();

            var newTransaction = new TransactionShopItem(this, transactionType, amount);
            Transactions.Add(newTransaction);

            return Task.CompletedTask;
        }
    }
}