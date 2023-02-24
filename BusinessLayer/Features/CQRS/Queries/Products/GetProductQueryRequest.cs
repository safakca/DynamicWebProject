using DtoLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Products;

public class GetProductQueryRequest : IRequest<List<ProductDto>> { }

