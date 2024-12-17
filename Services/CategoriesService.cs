using System.Text.Json;
using MyBlazorApp.Models;
using MyBlazorApp.Services.Interfaces;

namespace MyBlazorApp.Services;

public class CategoriesService : ICategoriesService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonSerializerOptions;

    public CategoriesService(HttpClient client)
    {
        this.client = client;
        this.jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Category>?> GetAsync()
    {
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Category>>(content, jsonSerializerOptions);
    }
}