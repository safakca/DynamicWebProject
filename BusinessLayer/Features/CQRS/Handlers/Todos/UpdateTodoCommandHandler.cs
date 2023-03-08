using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Todos;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Todos;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Todos;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommandRequest, UpdateTodoDto>
{
    private readonly IRepository<Todo> _repository;
    private readonly IMapper _mapper;
    public UpdateTodoCommandHandler(IRepository<Todo> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateTodoDto> Handle(UpdateTodoCommandRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetAsync(request.Id);
        todo.Title = request.Title;
        todo.Description = request.Description; 
        //todo.Status = (int)request.Status;
        todo.UpdatedDate = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(todo);
        var mapped = _mapper.Map<UpdateTodoDto>(updated);
        return mapped;
    }
}
