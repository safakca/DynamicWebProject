using System;
using BusinessLayer.Features.CQRS.Commands.Products;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
    private readonly IRepository<Product> _repository;

    public UpdateProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var updated = await _repository.GetAsync(request.Id);
        if (updated != null)
        {
            updated.Name = request.Name;
            updated.Price = request.Price;
            updated.Stock = request.Stock;
            await _repository.UpdateAsync(updated);
        }
        return Unit.Value;
    }
}

