using Application.Common.Catalogue.Queries;
using AutoMapper;
using Domain.Entities;
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
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public CreateProductDto CreateProduct { get; set; }

        public CreateProductCommand(CreateProductDto createProduct)
        {
            CreateProduct = createProduct;
        }

    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IApplicationDbcontext _context)
        {
            RuleFor(p => p.CreateProduct)
            .NotNull()
            .WithMessage("Product details are required.");

            RuleFor(p => p.CreateProduct.Title)
                .NotEmpty()
                .MaximumLength(75);

            RuleFor(p => p.CreateProduct.Sku)
                .NotEmpty()
                .MaximumLength(10);



            RuleFor(p => new { p.CreateProduct.Sku, p.CreateProduct.Category })
                .MustAsync(async (x, ct) =>
                   await _context.Products
                     .FirstOrDefaultAsync(c => c.Sku == x.Sku && c.Category.Id == x.Category.Id, ct) is null)
            .WithMessage(x => $"Product '{x.CreateProduct.Sku}' with Product '{x.CreateProduct.Category.Name}' already exists.");
        }
    }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IApplicationDbcontext _context;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateProduct == null)
            {
                throw new ArgumentNullException(nameof(request.CreateProduct), "CreateProductDto is null");
            }

            var product = _mapper.Map<Product>(request.CreateProduct);
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
