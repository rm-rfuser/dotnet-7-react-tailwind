namespace WebApi.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public ArticleType Type { get; set; }
}