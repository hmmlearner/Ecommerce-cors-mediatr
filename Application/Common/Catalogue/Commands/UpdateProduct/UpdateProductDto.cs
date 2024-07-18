using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Catalogue.Commands
{
    public record UpdateProductDto
    {
        public Guid Id { get; set; }
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
                CreateMap<UpdateProductDto, Product>();
            }
        }
    }
}
