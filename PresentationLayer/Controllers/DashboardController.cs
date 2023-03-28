using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }



    public async Task<IActionResult> Weather()
    {
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
            var body = await response.Content.ReadAsStringAsync();
        }

        return View();
    }


}
