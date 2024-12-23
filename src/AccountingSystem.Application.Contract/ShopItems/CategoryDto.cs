using System;

namespace AccountingSystem.Application.Contract.ShopItems
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}