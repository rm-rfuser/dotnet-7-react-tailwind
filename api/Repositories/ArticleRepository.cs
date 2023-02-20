namespace WebApi.Repositories;

using Dapper;
using WebApi.Entities;
using WebApi.Helpers;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAll();
    Task<Article> GetById(int id);
    Task<Article> GetByType(string type);
    Task Create(Article article);
    Task Update(Article article);
    Task Delete(int id);
}

public class ArticleRepository : IArticleRepository
{
    private DataContext _context;

    public ArticleRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Article
        """;
        return await connection.QueryAsync<Article>(sql);
    }

    public async Task<Article> GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Article
            WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Article>(sql, new { id });
    }

    public async Task<Article> GetByType(string type)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Article
            WHERE [Type] = @type
        """;
        return await connection.QuerySingleOrDefaultAsync<Article>(sql, new { type });
    }

    public async Task Create(Article article)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Article (Title, Description, ImageUrl, Type)
            VALUES (@Title, @Description, @ImageUrl, @Type)
        """;
        await connection.ExecuteAsync(sql, article);
    }

    public async Task Update(Article article)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE Article
            SET Title = @Title,
                Description = @Description,
                ImageUrl = @ImageUrl, 
                Type = @Type
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, article);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM Article
            WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}