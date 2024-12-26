using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Application.Contract.Transactions;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.ShopItems;
using AccountingSystem.Domain.Transactions;
using TransactionType = AccountingSystem.Application.Contract.Transactions.TransactionType;

namespace AccountingSystem.Application.Transactions
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly IRepository<TransactionShopItem, Guid> _transactionRepository;
        private readonly IRepository<ShopItem, Guid> _shopItemRepository;

        public TransactionAppService(
            IRepository<TransactionShopItem, Guid> transactionRepository,
            IRepository<ShopItem, Guid> shopItemRepository)
        {
            _transactionRepository = transactionRepository;
            _shopItemRepository = shopItemRepository;
        }

        private static TransactionDto MapTransaction(TransactionShopItem transaction)
        {
            var shopItem = transaction.ShopItem;
            var intType = (int)transaction.Type;
            
            var result = new TransactionDto
            {
                Id = transaction.Id,
                CreateAt = transaction.CreateAt,
                ShopItem = new ShopItemDto
                {
                    Id = shopItem.Id,
                    Name = shopItem.Name,
                    Description = shopItem.Description,
                    Price = shopItem.Price,
                    Category = new CategoryDto()
                    {
                        Id = shopItem.Category.Id,
                        Title = shopItem.Category.Title,
                        Description = shopItem.Category.Description,
                    }
                },
                Type = (TransactionType)intType,
                Amount = transaction.Amount,
            };
            
            return result;
        }
        
        public async Task<List<TransactionDto>> GetListAsync()
        {
            var transactions = (await _transactionRepository.WithDetails(c => c.ShopItem))
                .ToList();
            
            var items = transactions
                .Select(MapTransaction)
                .ToList();
            
            return items;
        }

        public async Task AddTransaction(CreateTransactionInput transaction)
        {
            var shopItem = await _shopItemRepository.GetByIdAsync(transaction.ShopItemId);
            var intType = (int)transaction.Type;
            
            await shopItem.AddTransaction((Domain.Transactions.TransactionType)intType, transaction.Amount);
            
            await _shopItemRepository.UpdateAsync(shopItem);
        }
    }
}