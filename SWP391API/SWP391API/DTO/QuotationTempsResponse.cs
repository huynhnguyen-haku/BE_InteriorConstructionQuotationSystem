using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391API.Models;

namespace SWP391API.DTO
{
    public class QuotationTempsResponse 
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }

        public QuotationTempsResponse(QuotationTemp q)
        {
            UserId = q.UserId;
            ProductId = q.ProductId;
            Quantity = q.Quantity;
            Name = q.Product.Name;
            Price = q.Product.Price;
            Description = q.Product.Description;
            Size = q.Product.Size;
            ImageUrl = q.Product.ImageUrl;
        }
    }
}
