using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;
public class ArticleController : Controller
{
    #region Ctor
    private readonly IHttpClientFactory _httpClientFactory;
    public ArticleController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
    #endregion

    #region List
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5097/api/Articles/GetAll");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<ArticleListModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(result);
        }

        return View();
    }
    #endregion

    #region Detail
    public async Task<IActionResult> Details(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:5097/api/Articles/Get/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ArticleListModel>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(result);
        }

        return View();
    }

    #endregion

    #region CreateGet
    [HttpGet]
    public IActionResult Create()
    { 
        return View(new CreateArticleModel());
    }
    #endregion

    #region CreatePost
    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleModel model)
    { 
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5097/api/Articles/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Happened a error");
            }
        }  
        return View(model);
    }
    #endregion

    #region UpdateGet
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync($"http://localhost:5097/api/Articles/Get/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UpdateArticleModel>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(result);
        }


        return RedirectToAction("List");
    }

    #endregion

    #region UpdatePost
    [HttpPost]
    public async Task<IActionResult> Update(UpdateArticleModel model)
    {
        if (ModelState.IsValid)
        {

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:5097/api/Articles/Update", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Happened a error");
            } 
        }
        return View(model);
    }

    #endregion

    #region DeleteGet   

    [HttpPost]
    public async Task<IActionResult> DeletePost(int id)
    {

        var client = _httpClientFactory.CreateClient();

        var response = await client.DeleteAsync($"http://localhost:5097/api/Articles/Delete/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("List");
        }

        return RedirectToAction("List");
    }

    #endregion

    #region DeletePost
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:5097/api/Articles/Get/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ArticleListModel>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(result);
        }

        return View();
    }
    #endregion


}