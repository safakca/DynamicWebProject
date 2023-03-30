using Azure;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PresentationLayer.Models.Weather;

namespace PresentationLayer.Controllers;
public class DashboardController : Controller
{
    private readonly IRepository<Article> _articleRepository;
    private readonly IRepository<Todo> _todoRepository;

    public DashboardController(IRepository<Article> articleRepository, IRepository<Todo> todoRepository)
    {
        _articleRepository = articleRepository;
        _todoRepository = todoRepository;
    }

    public async Task<IActionResult> Index()
    {
        List<Article> articleList = await _articleRepository.GetAllAsync();
        ViewBag.Article = articleList;

        List<Todo> todos= await _todoRepository.GetAllAsync();
        ViewBag.Todos = todos;
 
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://weather-by-api-ninjas.p.rapidapi.com/v1/weather?city=Istanbul&country=Turkey"),
            Headers =
            {
                { "X-RapidAPI-Key", "b881181406msh32a0ead044431adp1d4417jsn2e90cd1f8a96" },
                { "X-RapidAPI-Host", "weather-by-api-ninjas.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode == true)
            {
                string body = await response.Content.ReadAsStringAsync();
                // should be use deserialize to get
                // should be use serialize to update and create 
                WeatherViewModel weathers = JsonConvert.DeserializeObject<WeatherViewModel>(body);
                ViewBag.Weather = weathers;
            } 
        } 



        return View();
    } 
     

}


 