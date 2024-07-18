using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Catalogue.Queries.GetProduct
{
        public class GetProduct : IRequest<ProductDto>
        {
            public Guid Id { get; set; } = default!;

            public GetProduct(Guid id)
            {
                Id = id;
            }
        }



        public class GetProductRequestHandler : IRequestHandler<GetProduct, ProductDto>
        {
            private readonly IApplicationDbcontext _context;

            private readonly IMapper _mapper;
            public GetProductRequestHandler(IApplicationDbcontext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(GetProduct request, CancellationToken cancellationToken)
            {
                var result = await _context.Products
                            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (result == null)
                    throw new KeyNotFoundException();

                return result;
            }
        }

}
