using AutoMapper;
using Domain.Entities;

namespace Application.Common.Catalogue.Commands
{
    public record CreateProductDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Sku { get; set; } = default!;
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public double WasPrice { get; set; }
        public string ImageUrl { get; set; } = default!;
        public int Inventory { get; set; }
        public virtual Category Category { get; set; } = default!;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateProductDto, Product>();
            }
        }

    }
}
