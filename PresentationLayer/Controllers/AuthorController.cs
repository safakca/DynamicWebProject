using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Validations;
using System.Text;
using System.Text.Json;

namespace PresentationLayer.Controllers;
public class AuthorController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthorController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

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

    #region CreateGet
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(new CreateAuthorModel());


        //var model = new CreateAuthorModel();
        //var client =_httpClientFactory.CreateClient();
        //var response = await client.GetAsync("http://localhost:5097/api/Authors/Create");

        //if (response.IsSuccessStatusCode)
        //{
        //    var jsonData = await response.Content.ReadAsStringAsync();

        //    var data = JsonSerializer.Deserialize<List<AuthorListModel>>(jsonData, new JsonSerializerOptions
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    });

        //    return View(model);
        //}
        //return RedirectToAction("List");
    }
    #endregion

    #region CreatePost
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorModel model)
    {
        AuthorValidator validator = new AuthorValidator();
        ValidationResult result=validator.Validate(model);
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
        else
        {
            foreach (ValidationFailure failer in result.Errors)
            {
                ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
            }
        }
        return View(model);
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> Update(int id)
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


        return RedirectToAction("List");
    }


    [HttpPost]
    public async Task<IActionResult> Update(AuthorListModel model)
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


    public async Task<IActionResult> Remove(int id)
    {

        var client = _httpClientFactory.CreateClient(); 

        var response = await client.DeleteAsync($"http://localhost:5097/api/Authors/Delete/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("List");
        }

        return RedirectToAction("List");
    }


}
