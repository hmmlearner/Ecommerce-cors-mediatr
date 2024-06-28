using AutoMapper;
using Domain.Entities;

namespace Application.Common.Catalogue.Queries
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int DisplayOrder { get;  set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Category, CategoryDto>();
                CreateMap<CategoryDto, Category>();
            }
        }
    }
}
