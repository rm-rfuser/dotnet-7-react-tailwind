namespace WebApi.Models.Article;

using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

#nullable enable
public class UpdateRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    [EnumDataType(typeof(ArticleType))]
    public string? Type { get; set; }
}