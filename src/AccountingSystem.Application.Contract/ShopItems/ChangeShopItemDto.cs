using System;

namespace AccountingSystem.Application.Contract.ShopItems
{
    public class ChangeShopItemDto
    {
        public Guid CategoryId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}