using System;
using BusinessLayer.Features.CQRS.Commands.Products;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
{
    private readonly IRepository<Product> _repository;

    public CreateProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _repository.CreateAsync(new Product
        {
            Name=request.Name,
            Price=request.Price,
            Stock=request.Stock,
        });
        return Unit.Value;
    }
}