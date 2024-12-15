using System;
using System.Collections.Generic;
using AccountingSystem.Domain.Core;

namespace AccountingSystem.Domain.ShopItems
{
    public class Category : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ShopItem> ShopItems { get; private set; }
        
        public Category() {}
        public Category(Guid id) : base(id) {}
        public Category(string title, string description)
        {
            Title = title;
            Description = description;
            ShopItems = new List<ShopItem>();
        }
    }
}