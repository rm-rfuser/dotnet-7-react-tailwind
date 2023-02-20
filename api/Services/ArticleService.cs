namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Article;
using WebApi.Repositories;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAll();
    Task<Article> GetById(int id);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}

public class ArticleService : IArticleService
{
    private IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public ArticleService(
        IArticleRepository articleRepository,
        IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Article>> GetAll()
    {
        return await _articleRepository.GetAll();
    }

    public async Task<Article> GetById(int id)
    {
        var article = await _articleRepository.GetById(id);

        if (article == null)
            throw new KeyNotFoundException("Article not found");

        return article;
    }

    public async Task Create(CreateRequest model)
    {
        var article = _mapper.Map<Article>(model);
        await _articleRepository.Create(article);
    }

    public async Task Update(int id, UpdateRequest model)
    {
        var article = await _articleRepository.GetById(id);

        if (article == null)
            throw new KeyNotFoundException("Article not found");

        _mapper.Map(model, article);

        await _articleRepository.Update(article);
    }

    public async Task Delete(int id)
    {
        await _articleRepository.Delete(id);
    }
}