using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Products;

public record DeleteProductCommandRequest(int Id) : IRequest { }

