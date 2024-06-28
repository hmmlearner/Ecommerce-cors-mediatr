using MediatR;
using Domain.Common.Contracts;
using FluentValidation;
using Domain.Entities;
using AutoMapper;
using Infrastructure.Persistence;
using Application.Common.Catalogue.Queries;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Catalogue.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public CreateCategoryDto CreateCategory { get; set; }

        public CreateCategoryCommand(CreateCategoryDto createCategory)
        {
            CreateCategory = createCategory;
        }

    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IApplicationDbcontext _context)
        {
            RuleFor(p => p.CreateCategory)
            .NotNull()
            .WithMessage("Category details are required.");

            RuleFor(p => p.CreateCategory.Name)
                .NotEmpty()
                .MaximumLength(75);


            RuleFor(p => new { p.CreateCategory.Name, p.CreateCategory.DisplayOrder })
                .MustAsync(async (x, ct) =>
                   await _context.Categories
                     .FirstOrDefaultAsync(c => c.Name == x.Name && c.DisplayOrder == x.DisplayOrder, ct) is null)
            .WithMessage(x => $"Category '{x.CreateCategory.Name}' with display order '{x.CreateCategory.DisplayOrder}' already exists.");
        }
    }


    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IApplicationDbcontext _context;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateCategory == null)
            {
                throw new ArgumentNullException(nameof(request.CreateCategory), "CreateCategoryDto is null");
            }

            var category = _mapper.Map<Category>(request.CreateCategory);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
