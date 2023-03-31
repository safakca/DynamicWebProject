using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text.Json;

namespace PresentationLayer.Controllers
{
    public class FakediumController : Controller
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public FakediumController(IRepository<Article> articleRepository, IRepository<Author> authorRepository, IHttpClientFactory httpClientFactory)
        {
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<Article> articleList = await _articleRepository.GetAllAsync(); 
            var topArticle = articleList.Take(3).ToList(); 
            ViewBag.Article = topArticle;

            List<Author> authorList = await _authorRepository.GetAllAsync();
            ViewBag.Author = authorList;
             
            return View();
        }

    
        public async Task<IActionResult> Details(int id)
        {

            //var client = _httpClientFactory.CreateClient();
            //var response = await client.GetAsync($"http://localhost:5097/api/Articles/Get/{id}");

            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonData = await response.Content.ReadAsStringAsync();
            //    var result = JsonSerializer.Deserialize<ArticleListModel>(jsonData, new JsonSerializerOptions
            //    {
            //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //    });

            //    return View(result);
            //}

            //return View();


            var article = await _articleRepository.GetAsync(id);
            ViewBag.Article = article;
            return View();
        }

    }
}
