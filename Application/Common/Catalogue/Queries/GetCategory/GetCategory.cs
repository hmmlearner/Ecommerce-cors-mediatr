using AutoMapper;
using Infrastructure.Persistence;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Catalogue.Queries
{
    public class GetCategory: IRequest<CategoryDto>
    {
        public Guid Id { get; set; } = default!;

        public GetCategory(Guid id)
        {
            Id = id;
        }
    }

 

    public class GetCategoryRequestHandler : IRequestHandler<GetCategory, CategoryDto>
    {
        private readonly IApplicationDbcontext _context;

        private readonly IMapper _mapper;
        public GetCategoryRequestHandler(IApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategory request, CancellationToken cancellationToken)
        {           
            var result = await _context.Categories
                        .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (result == null)
                throw new KeyNotFoundException();

            return result;
        }
    }

}
