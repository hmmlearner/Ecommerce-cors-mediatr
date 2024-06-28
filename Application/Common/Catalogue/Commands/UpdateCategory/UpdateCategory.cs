using Application.Common.Catalogue.Queries;
using AutoMapper;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Catalogue.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public UpdateCategoryDto UpdateCategory { get; set; } = default!;

        public UpdateCategoryCommand(Guid recipe, UpdateCategoryDto updateCategory)
        {
            Id = recipe;
            UpdateCategory = updateCategory;
        }
    }



    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IApplicationDbcontext _context)
        {
            RuleFor(p => p.UpdateCategory)
            .NotNull()
            .WithMessage("Category details are required.");

            RuleFor(p => p.UpdateCategory.Name)
                .NotEmpty()
                .MaximumLength(75);


            RuleFor(p => new { p.UpdateCategory.Name, p.UpdateCategory.DisplayOrder })
                .MustAsync(async (x, ct) =>
                   await _context.Categories
                     .FirstOrDefaultAsync(c => c.Name == x.Name && c.DisplayOrder == x.DisplayOrder, ct) is null)
            .WithMessage(x => $"Category '{x.UpdateCategory.Name}' with display order '{x.UpdateCategory.DisplayOrder}' already exists.");
        }
    }


    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IApplicationDbcontext _context;

        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(IApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(r => r.Id == request.UpdateCategory.Id);

            if (category == null)
                throw new KeyNotFoundException();

            _mapper.Map(request.UpdateCategory, category);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }   



}
