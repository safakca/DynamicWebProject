using DtoLayer.Concrete.Authors;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Authors;

public class GetAuthorByIdQueryRequest : IRequest<AuthorsDto>
{
    public int Id { get; set; }
    public GetAuthorByIdQueryRequest(int id)
    {
        Id = id;
    }
}