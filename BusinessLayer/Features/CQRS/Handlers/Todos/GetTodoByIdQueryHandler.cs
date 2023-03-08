using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Todos;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Todos;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Todos;

public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQueryRequest, TodosDto>
{
    private readonly IRepository<Todo> _repostiroy;
    private readonly IMapper _mapper;

    public GetTodoByIdQueryHandler(IRepository<Todo> repostiroy, IMapper mapper)
    {
        _repostiroy = repostiroy;
        _mapper = mapper;
    }

    public async Task<TodosDto> Handle(GetTodoByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repostiroy.GetByFilterAsync(x => x.Id == request.Id);
        return _mapper.Map<TodosDto>(data);
    }
}