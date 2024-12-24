using System;

namespace AccountingSystem.Application.Contract.ShopItems
{
    public class ShopItemDto
    {
        public Guid Id { get; set; }
        public CategoryDto Category { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}