using DtoLayer.Concrete.Authors;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Authors;

public record UpdateAuthorCommandRequest : IRequest<UpdateAuthorDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}
