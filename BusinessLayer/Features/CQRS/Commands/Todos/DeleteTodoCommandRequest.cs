using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Todos;

public record DeleteTodoCommandRequest(int Id) : IRequest { }