using Application.Common.Catalogue.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Catalogue.Commands
{
    public record CreateCategoryDto
    {
        public string Name { get; set; } = default!;
        public int DisplayOrder { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateCategoryDto, Category>();
            }
        }
    }
}
