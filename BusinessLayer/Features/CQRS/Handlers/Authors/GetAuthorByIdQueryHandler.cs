using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Authors;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Authors;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQueryRequest, AuthorsDto>
{
    private readonly IRepository<Author> _repostiroy;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(IRepository<Author> repostiroy, IMapper mapper)
    {
        _repostiroy = repostiroy;
        _mapper = mapper;
    }

    public async Task<AuthorsDto> Handle(GetAuthorByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repostiroy.GetByFilterAsync(x => x.Id == request.Id);
        return _mapper.Map<AuthorsDto>(data);
    }
}