using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Authors;

public record DeleteAuthorCommandRequest(int Id) : IRequest { }