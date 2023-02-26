using DtoLayer.Concrete.Authors;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Authors;

public class GetAuthorsQueryRequest : IRequest<List<AuthorsDto>> { }
