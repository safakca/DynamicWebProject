using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc; 
using System.Text.Json;

namespace PresentationLayer.Controllers
{
    public class FakediumController : Controller
    {
        private readonly IRepository<Article> _articleRepository; 

        public FakediumController(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository; 
        }

        public async Task<IActionResult> Index()
        {
            List<Article> articleList = await _articleRepository.GetAllAsync(); 
            var topArticle = articleList.Take(3).ToList(); 
            ViewBag.Article = topArticle; 
            return View();
        }

    
        public async Task<IActionResult> Details(int id)
        { 
            var article = await _articleRepository.GetAsync(id);
            ViewBag.Article = article;
            return View();
        }

        public IActionResult ChatBot()
        { 
            return View();
        }

    }
}
