using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Authors;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Authors;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQueryRequest, List<AuthorsDto>>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public GetAuthorsQueryHandler(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AuthorsDto>> Handle(GetAuthorsQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<List<AuthorsDto>>(data);
    }
}
