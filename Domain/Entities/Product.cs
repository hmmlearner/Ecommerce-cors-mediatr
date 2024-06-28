using Domain.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product: AuditableEntity, IAggregateRoot
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public string Sku { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public double WasPrice { get; set; }
        public string ImageUrl { get; set; } 
        public Guid CategoryId { get; set; }
        public int Inventory { get; set; }
        public virtual Category Category { get; set; } = default!;

        public Product(string title, string description, string sku, double price, double salePrice, double wasPrice, string imageUrl, Guid categoryId, int inventory) 
        {
            Title = title;
            Description = description;
            Sku = sku;
            Price = price;
            SalePrice = salePrice;
            WasPrice = wasPrice;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            Inventory = inventory;
        }

        public void Update(string title, string description, string sku, double price, double salePrice, double wasPrice, string imageUrl, Guid categoryId, int inventory)
        {
            if(!string.IsNullOrEmpty(title) && !Title.Equals(title)) Title = title;
            if(!string.IsNullOrEmpty(description) && !Description.Equals(description)) Description = description;
            if(!string.IsNullOrEmpty(sku) && !Sku.Equals(sku)) Sku = sku;
            if(price > 0) Price = price;
            if(salePrice > 0) SalePrice = salePrice;
            if(wasPrice > 0) WasPrice = wasPrice;
            if(!string.IsNullOrEmpty(imageUrl) && !ImageUrl.Equals(imageUrl)) ImageUrl = imageUrl;
            if(CategoryId != categoryId) CategoryId = categoryId;
            if(inventory > 0) Inventory = inventory;
        }
    }
}
