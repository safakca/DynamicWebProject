using BusinessLayer.Features.CQRS.Queries;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Jwt;
using EntityLayer.Concrete;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
        //var user = await _userRepository.GetByFilterAsync(x => x.UserName == request.Username && x.Password == request.Password);
        var user = await _userManager.Users.ToListAsync();

        if (user == null)
        {
            dto.IsExist = false;
        }
        else
        {

            //dto.Username = request.Username;
            //dto.Id = user.Id;
            //dto.IsExist = true;
            //var role = await _roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
            //dto.Role = role?.Defination;
        }
        return dto;
    }
}
