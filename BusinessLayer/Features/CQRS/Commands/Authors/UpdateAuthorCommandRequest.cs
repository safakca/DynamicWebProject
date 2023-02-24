using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Authors;

public class UpdateAuthorCommandRequest : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}
