using System;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Products;

public class CreateProductCommandRequest : IRequest
{
    public string? Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}

