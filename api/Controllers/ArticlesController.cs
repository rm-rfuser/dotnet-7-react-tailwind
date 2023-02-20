namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Article;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class ArticlesController : ControllerBase
{
    private IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _articleService.GetAll();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _articleService.GetById(id);
        return Ok(article);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequest model)
    {
        await _articleService.Create(model);
        return Ok(new { message = "Article created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest model)
    {
        await _articleService.Update(id, model);
        return Ok(new { message = "Article updated" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _articleService.Delete(id);
        return Ok(new { message = "Article deleted" });
    }
}