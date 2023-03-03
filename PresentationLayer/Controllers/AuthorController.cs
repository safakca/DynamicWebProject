using BusinessLayer.Repositories;
using ClosedXML.Excel;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text;
using System.Text.Json;

namespace PresentationLayer.Controllers;


public class AuthorController : Controller
{
    #region Ctor
    private readonly IHttpClientFactory _httpClientFactory;    private readonly IRepository<Author> _repository;    public AuthorController(IHttpClientFactory httpClientFactory, IRepository<Author> repository)    {        _httpClientFactory = httpClientFactory;        _repository = repository;    }

    #endregion

    #region List
    [Authorize(Roles = "Admin, Member")]
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
        }
        return View();
    }
    #endregion

    #region Detail
    [Authorize(Roles = "Admin, Member")]
    public async Task<IActionResult> Details(int id)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
        }
        return View();
    }

    #endregion

    #region CreateGet
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateAuthorModel());
    }
    #endregion

    #region CreatePost
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorModel model)
    {
        if (ModelState.IsValid)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                var jsonData = JsonSerializer.Serialize(model);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5097/api/Authors/Create", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Author");
                }
                else
                {
                    ModelState.AddModelError("", "Dublicate Author Name! ");
                }
            }
            else
            {
                ModelState.AddModelError("", "Token is null! ");
            }
        }
        return View(model);
    }
    #endregion

    #region UpdateGet
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
        }

        return RedirectToAction("List");
    }

    #endregion

    #region UpdatePost
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Update(UpdateAuthorModel model)
    {
        if (ModelState.IsValid)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
        }
        return View(model);
    }

    #endregion

    #region DeletePost   
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> DeletePost(int id)
    {

        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"http://localhost:5097/api/Authors/Delete/{id}");
        }
        return RedirectToAction("List");
    }

    #endregion

    #region DeleteGet
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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
        }
        return View();
    }
    #endregion

    #region DowloandExcel
    [Authorize(Roles = "Admin, Member")]
    public async Task<ActionResult> DownloadExcel()
    {
        var Authors = await _repository.GetAllAsync();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Authors");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "Name";
            worksheet.Cell(currentRow, 2).Value = "Surname";
            worksheet.Cell(currentRow, 3).Value = "Age";
            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
            worksheet.Cell(currentRow, 5).Value = "UpdatedDate";

            foreach (var author in Authors)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value += author.Name;
                worksheet.Cell(currentRow, 2).Value += author.Surname;
                worksheet.Cell(currentRow, 3).Value += author.Age.ToString();
                worksheet.Cell(currentRow, 4).Value += author.CreatedDate.ToString();
                worksheet.Cell(currentRow, 5).Value += author.UpdatedDate.ToString();
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Author.xlsx");
            }
        }
    }
    #endregion
}
