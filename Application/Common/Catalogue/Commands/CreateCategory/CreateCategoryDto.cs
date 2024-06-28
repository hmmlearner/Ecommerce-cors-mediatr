using Application.Common.Catalogue.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
