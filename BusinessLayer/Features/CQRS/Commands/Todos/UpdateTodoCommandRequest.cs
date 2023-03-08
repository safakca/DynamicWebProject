using DtoLayer.Concrete.Todos;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Todos;

public record UpdateTodoCommandRequest : IRequest<UpdateTodoDto>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
}
