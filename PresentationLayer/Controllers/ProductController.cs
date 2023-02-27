using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text.Json;

namespace PresentationLayer.Controllers;

public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> GetAll()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5097/api/Product/getall");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<ProductListModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return View(result);
        }
        return View();
    }
}