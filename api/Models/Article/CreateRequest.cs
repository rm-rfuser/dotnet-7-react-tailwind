namespace WebApi.Models.Article;

using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

public class CreateRequest
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Required]
    [EnumDataType(typeof(ArticleType))]
    public string Type { get; set; }
}