using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

public class AccountController : Controller
{
    #region Ctor
    private readonly IHttpClientFactory _httpClientFactory;
    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    #endregion

    #region LoginGet
    public IActionResult Login()
    {
        return View(new UserLoginModel());
    }
    #endregion

    #region LoginPost
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5097/api/Auth/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new();

                    var token = handler.ReadJwtToken(tokenModel.Token);

                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null)
                        claims.Add(new Claim("accessToken", tokenModel.Token));

                    var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                    var authProps = new AuthenticationProperties
                    {
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true  
                    };
                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Username or password wrong ! ");
            }
        }
        return View(model);
    }
    #endregion

    #region Logout
    public IActionResult Logout()
    {
        //await _signInManager.SignOutAsync();

        return RedirectToAction("Login");
    }
    #endregion

    #region RegisterGet 
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    #endregion

    #region RegisterPost
    [HttpPost]
    public IActionResult Register(AppUserRegisterViewModel appUserRegisterViewModel)
    {
        if (ModelState.IsValid)
        {
            AppUser appUser = new AppUser()
            { 
                UserName = appUserRegisterViewModel.Username,
                Password= appUserRegisterViewModel.Password, 
            };

            if (appUserRegisterViewModel.Password == appUserRegisterViewModel.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, appUserRegisterViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Password Not Combine");
            }
        }
        return View();
    } 
    #endregion

}
