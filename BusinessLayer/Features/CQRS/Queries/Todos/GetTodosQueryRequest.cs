using DtoLayer.Concrete.Todos;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Todos;

public class GetTodosQueryRequest : IRequest<List<TodosDto>> { }
