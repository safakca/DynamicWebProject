using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
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
    //private readonly IRepository<AppUser> _repository;
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

                    return RedirectToAction("List", "Author");
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
        ModelState.Clear();
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

            model.MailCode = new Random().Next(10000, 999999).ToString();

            if (model.Password == model.ConfirmPassword)
            {

                var response = await client.PostAsync("http://localhost:5097/api/Auth/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    SendEmail(model.Email, model.MailCode);
                    //return RedirectToAction("Login", "Account");
                    return RedirectToAction("EmailConfirmed", "Register");
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
        //var user = await _repository.GetByFilterAsync(x => x.Email == appUser.Email);

        //if (user.MailCode == appUser.MailCode)
        //{
        //    user.EmailConfirmed = true;

        //    var result = await _repository.UpdateAsync(user);
        //    return RedirectToAction("Index", "Login");
        //}
        return View();
    }


    #region SendMail
    public void SendEmail(string email, string emailcode)
    {
        MimeMessage mimeMessage = new MimeMessage();

        MailboxAddress mailboxAddressFrom = new MailboxAddress("Member", "safakcatest@gmail.com");
        mimeMessage.From.Add(mailboxAddressFrom);

        MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
        mimeMessage.To.Add(mailboxAddressTo);

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = emailcode;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        mimeMessage.Subject = "Register Form";

        SmtpClient smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, true);
        smtp.Authenticate("safakcatest@gmail.com", "testSifre123**");
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
