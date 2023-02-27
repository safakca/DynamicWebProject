using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Authors;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Authors;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommandRequest, UpdateAuthorDto>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;
    public UpdateAuthorCommandHandler(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateAuthorDto> Handle(UpdateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetAsync(request.Id);
        author.Name = request.Name;
        author.Surname = request.Surname;
        author.Age = request.Age;
        author.UpdatedDate = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(author);
        var mapped = _mapper.Map<UpdateAuthorDto>(updated);
        return mapped;
    } 
}
