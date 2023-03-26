using BusinessLayer.Repositories;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PresentationLayer.Models;
using PresentationLayer.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace PresentationLayer.Controllers;

public class AccountController : Controller
{
    #region Ctor
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserManager<AppUser> _userManager;
    public AccountController(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager)
    {
        _httpClientFactory = httpClientFactory;
        _userManager = userManager;
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

                    return RedirectToAction("List", "Article");
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
        return RedirectToAction("Login");
    }
    #endregion

    #region RegisterGet 
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }
    #endregion

    #region RegisterPost
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");


            if (model.Password == model.ConfirmPassword)
            {
                var response = await client.PostAsync("http://localhost:5097/api/Auth/Register", content);

                var x = await _userManager.Users.OrderByDescending(x => x.Id).LastAsync();
                model.MailCode = x.MailCode;

                if (response.IsSuccessStatusCode)
                {
                    SendEmail(model.Email, model.MailCode);
                    return RedirectToAction("EmailConfirmed", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Dublicate Username! ");
                }
            }
            else
            {
                ModelState.AddModelError("", "Password and confirm password does not match ! ");
            }
        }
        return View(model);
    }


    #endregion


    [HttpGet]
    public IActionResult EmailConfirmed()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> EmailConfirmed(AppUser appUser)
    {
        var user = await _userManager.FindByEmailAsync(appUser.Email);
        if (user.MailCode == appUser.MailCode)
        {
            user.EmailConfirmed = true;

            var result = await _userManager.UpdateAsync(user);
            return RedirectToAction("Login", "Account");
        }
        return View();
    }


    #region SendMail
    public void SendEmail(string email, string emailcode)
    {
        MimeMessage mimeMessage = new MimeMessage();

        MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "safakcatest@gmail.com");
        mimeMessage.From.Add(mailboxAddressFrom);

        MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
        mimeMessage.To.Add(mailboxAddressTo);

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = emailcode;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        mimeMessage.Subject = "Register Form";

        SmtpClient smtp = new SmtpClient(); 
        smtp.Connect("smtp.gmail.com", 587, false);

        //google security key xx 
        smtp.Authenticate("xx@gmail.com", "xx");
        smtp.Send(mimeMessage);
        smtp.Disconnect(true);
    }
     

    #endregion

    #region AccessDenied
    public IActionResult AccessDenied()
    {
        return View();
    }
    #endregion
}
