using System.Text.Json;

namespace WebApi.Services;

public interface ISpaceflightService
{
    Task<IEnumerable<SpaceflightArticle>> GetAll();
}

public class SpaceflightService : ISpaceflightService
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _options;

    public SpaceflightService()
    {
        _httpClient = new();
        _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<IEnumerable<SpaceflightArticle>> GetAll()
    {
        using var response = await _httpClient.GetAsync("https://api.spaceflightnewsapi.net/v3/articles?_limit=1");

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<SpaceflightArticle>>(jsonString, _options);
    }
}

public class SpaceflightArticle
{
    public int Id { get; set; }

    public string Summary { get; set; }

    public string Title { get; set; }
}