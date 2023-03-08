using DtoLayer.Concrete.Todos;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Todos;
public record CreateTodoCommandRequest : IRequest<CreateTodoDto>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
}
