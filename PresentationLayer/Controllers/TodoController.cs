using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

public class TodoController : Controller
{
    #region Ctor
    private readonly IRepository<Todo> _repository;

    public TodoController(IRepository<Todo> repository)
    {
        _repository = repository;
    }
    #endregion

    #region List
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> List()
    {
        var result = await _repository.GetAllAsync();
        return View(result);
    }
    #endregion

    #region Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Todo todo)
    {
        await _repository.CreateAsync(todo);
        return RedirectToAction("List", "Todo");
    }
    #endregion

    #region Update
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _repository.GetAsync(id);
        return View(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Todo todo)
    {
        await _repository.UpdateAsync(todo);
        return RedirectToAction("List", "Todo");
    }

    #endregion

    #region Delete
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _repository.GetAsync(id);
        return View(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Todo todo)
    {
        await _repository.RemoveAsync(todo);
        return RedirectToAction("List", "Todo");
    }
    #endregion
}