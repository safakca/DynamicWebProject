using System;
using DtoLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries;

public class GetProductQueryRequest : IRequest<List<ProductDto>> { }

