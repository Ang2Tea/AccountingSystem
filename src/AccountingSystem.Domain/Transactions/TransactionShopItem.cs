using System;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.ShopItems;

namespace AccountingSystem.Domain.Transactions
{
    public class TransactionShopItem : Entity<Guid>
    {
        public DateTime CreateAt { get; private set; }

        public Guid ShopItemId { get; private set; }
        public ShopItem ShopItem { get; private set; }
        public TransactionType Type { get; private set; }
        public int Amount { get; private set; }

        public TransactionShopItem() { }
        public TransactionShopItem(Guid id): base(id) {}
        internal TransactionShopItem(ShopItem shopItem, TransactionType type, int amount)
        {
            CreateAt = DateTime.Now;
            ShopItemId = shopItem.Id;
            ShopItem = shopItem;
            Type = type; 
            Amount = amount;
        }

    }
}