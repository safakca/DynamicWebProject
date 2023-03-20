using BusinessLayer.Features.CQRS.Queries;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Jwt;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity; 

namespace BusinessLayer.Features.CQRS.Handlers.Users;
public class CheckUserRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager; 

    public CheckUserRequestHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
    {
        var dto = new CheckUserResponseDto();  
        var user = await _userManager.FindByNameAsync(request.Username); 
  
        if (user == null)
        {
            dto.IsExist = false;
        }
        else
        {
            dto.Username = request.Username; 
            dto.IsExist = true; 
        }
        return dto;
    }
}
