using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Articles;

public record DeleteArticleCommandRequest(int Id): IRequest { }