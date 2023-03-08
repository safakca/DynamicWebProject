using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Todos;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Todos;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Todos;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommandRequest, CreateTodoDto>
{
    private readonly IRepository<Todo> _repository;
    private readonly IMapper _mapper;

    public CreateTodoCommandHandler(IRepository<Todo> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateTodoDto> Handle(CreateTodoCommandRequest request, CancellationToken cancellationToken)
    {
        var todo = _mapper.Map<Todo>(request);
        todo.CreatedDate = DateTime.UtcNow; 
        var added = await _repository.CreateAsync(todo);
        var mapped = _mapper.Map<CreateTodoDto>(added);

        new Exception("Create is succeeded! ");
        return mapped;

    }
}
