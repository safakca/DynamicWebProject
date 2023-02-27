using BusinessLayer.Features.CQRS.Commands.Authors;
using BusinessLayer.Features.CQRS.Queries.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAuthorsQueryRequest()));

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _mediator.Send(new GetAuthorByIdQueryRequest(id)));

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id) => Ok(await _mediator.Send(new DeleteAuthorCommandRequest(id)));

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateAuthorCommandRequest request) => Created("", await _mediator.Send(request));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateAuthorCommandRequest request) => Ok(await _mediator.Send(request));
}
