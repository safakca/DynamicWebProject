using BusinessLayer.Features.CQRS.Commands.Users;
using BusinessLayer.Features.CQRS.Queries;
using DataAccessLayer.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommandRequest request)
    {
        return Created("", await _mediator.Send(request));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(CheckUserQueryRequest request)
    {
        var dto = await _mediator.Send(request);
        if (dto.IsExist)
        {
            return Created("", JwtGenerator.GenerateToken(dto));
        }
        else
        {
            return BadRequest("Username or password errror");
        }
    }
}
