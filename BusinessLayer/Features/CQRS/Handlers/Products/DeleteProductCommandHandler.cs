using System;
using BusinessLayer.Features.CQRS.Commands.Products;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Products;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
{
	private readonly IRepository<Product> _repository;

    public DeleteProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _repository.GetAsync(request.Id);
        if (deleted != null)
        {
            await _repository.RemoveAsync(deleted);
        }
        return Unit.Value;
    }
}

