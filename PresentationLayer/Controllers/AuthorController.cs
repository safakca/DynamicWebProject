using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text;
using System.Text.Json;

namespace PresentationLayer.Controllers;
public class AuthorController : Controller
{
    #region Ctor
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthorController(IHttpClientFactory httpClientFactory) =>_httpClientFactory = httpClientFactory;
    

    #endregion

    #region List
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5097/api/Authors/GetAll");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<AuthorListModel>>(jsonData, new JsonSerializerOptions
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
        var response = await client.GetAsync($"http://localhost:5097/api/Authors/Get/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthorListModel>(jsonData, new JsonSerializerOptions
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
        return View(new CreateAuthorModel());
    }
    #endregion

    #region CreatePost
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorModel model)
    { 
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5097/api/Authors/Create", content);

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

        var response = await client.GetAsync($"http://localhost:5097/api/Authors/Get/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UpdateAuthorModel>(jsonData, new JsonSerializerOptions
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
    public async Task<IActionResult> Update(UpdateAuthorModel model)
    {
        if (ModelState.IsValid)
        { 
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:5097/api/Authors/Update", content);

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

        var response = await client.DeleteAsync($"http://localhost:5097/api/Authors/Delete/{id}");
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
        var response = await client.GetAsync($"http://localhost:5097/api/Authors/Get/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthorListModel>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return View(result);
        }

        return View();
    } 
    #endregion
}
