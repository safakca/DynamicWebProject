using BusinessLayer.Repositories;
using ClosedXML.Excel;
using EntityLayer.Concrete; 
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text;
using System.Text.Json;

namespace PresentationLayer.Controllers;

public class ArticleController : Controller
{
    #region Ctor
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IRepository<Article> _repository;

    public ArticleController(IHttpClientFactory httpClientFactory, IRepository<Article> repository)
    {
        _httpClientFactory = httpClientFactory;
        _repository = repository;
    }

    #endregion

    #region List
    //[Authorize(Roles = "Admin, Member")]
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
    //[Authorize(Roles = "Admin, Member")]
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
    //[Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateArticleModel());
    }
    #endregion

    #region CreatePost
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
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
     
    #region DowloandExcel 
    public async Task<ActionResult> DownloadExcel()
    {
        var articles = await _repository.GetAllAsync();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Articles");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "Title";
            worksheet.Cell(currentRow, 2).Value = "Description"; 
            worksheet.Cell(currentRow, 3).Value = "CreatedDate";
            worksheet.Cell(currentRow, 4).Value = "UpdatedDate";

            foreach (var item in articles)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value += item.Title;
                worksheet.Cell(currentRow, 2).Value += item.Description; 
                worksheet.Cell(currentRow, 3).Value += item.CreatedDate.ToString();
                worksheet.Cell(currentRow, 4).Value += item.UpdatedDate.ToString();
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Article.xlsx");
            }
        }
    }
    #endregion
}