using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Todos;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Todos;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Todos;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQueryRequest, List<TodosDto>>
{
    private readonly IRepository<Todo> _repository;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IRepository<Todo> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TodosDto>> Handle(GetTodosQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<List<TodosDto>>(data);
    }
}
