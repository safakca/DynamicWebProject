using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace PresentationLayer.Controllers;
public class DashboardController : Controller
{
    private readonly IRepository<Article> _articleRepository;

    public DashboardController(IRepository<Article> articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IActionResult> Index()
    {
        List<Article> articleList = await _articleRepository.GetAllAsync();
        ViewBag.Article = articleList;

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/landon"),
            Headers =
            {
                { "X-RapidAPI-Key", "b881181406msh32a0ead044431adp1d4417jsn2e90cd1f8a96" },
                { "X-RapidAPI-Host", "open-weather13.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            string body = await response.Content.ReadAsStringAsync();
            JObject valuePairs = JObject.Parse(body);   
            ViewBag.Weater = valuePairs;
        } 

        return View();
    } 
     

}
