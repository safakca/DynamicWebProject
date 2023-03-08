using DtoLayer.Concrete.Todos;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Todos;

public class GetTodoByIdQueryRequest : IRequest<TodosDto>
{
    public int Id { get; set; }
    public GetTodoByIdQueryRequest(int id)
    {
        Id = id;
    }
}