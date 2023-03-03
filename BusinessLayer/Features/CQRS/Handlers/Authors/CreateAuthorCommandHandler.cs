using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Authors;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Authors;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommandRequest, CreateAuthorDto>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateAuthorDto> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var exist = await _repository.GetAllAsync();
        if (exist.Any(x => x.Name == request.Name))
        {
            throw new Exception("Dublicate Author Name!");
        }

        var author = _mapper.Map<Author>(request);
        author.CreatedDate = DateTime.UtcNow;
        var added = await _repository.CreateAsync(author);
        var mapped = _mapper.Map<CreateAuthorDto>(added);

        new Exception("Create is succeeded! ");
        return mapped;

    }
}
