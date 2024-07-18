using AutoMapper;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Catalogue.Commands
{
    public class UpdateProductCommand: IRequest<bool>
    {
        
        public Guid Id { get; set; }
        public UpdateProductDto UpdateProduct { get; set; } = default!;

        public UpdateProductCommand(Guid id, UpdateProductDto updateProduct)
        {
            Id = id;
            UpdateProduct = updateProduct;
        }
    }



    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(IApplicationDbcontext _context)
        {
            RuleFor(p => p.UpdateProduct)
            .NotNull()
            .WithMessage("Product details are required.");

            RuleFor(p => p.UpdateProduct.Title)
                .NotEmpty()
                .MaximumLength(75);


            RuleFor(p => new { p.UpdateProduct.Sku, p.UpdateProduct.Category })
               .MustAsync(async (x, ct) =>
                  await _context.Products
                    .FirstOrDefaultAsync(c => c.Sku == x.Sku && c.Category.Id == x.Category.Id, ct) is null)
          .WithMessage(x => $"Product '{x.UpdateProduct.Title}' with SKU '{x.UpdateProduct.Sku}' and category '{x.UpdateProduct.Category.Name}' already exists.");
        }
    }


    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IApplicationDbcontext _context;

        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(r => r.Id == request.UpdateProduct.Id);

            if (product == null)
                throw new KeyNotFoundException();

            _mapper.Map(request.UpdateProduct, product);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }


}

