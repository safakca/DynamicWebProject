using BusinessLayer.Features.CQRS.Commands.Products;
using BusinessLayer.Features.CQRS.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetProductQueryRequest()));

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _mediator.Send(new GetProductByIdQueryRequest(id)));

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id) => Ok(await _mediator.Send(new DeleteProductCommandRequest(id)));

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateProductCommandRequest request) => Created("", await _mediator.Send(request));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateProductCommandRequest request) => Ok(await _mediator.Send(request));
}
