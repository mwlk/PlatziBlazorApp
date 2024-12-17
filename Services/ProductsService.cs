using System.Net.Http.Json;
using System.Text.Json;
using MyBlazorApp.Models;
using MyBlazorApp.Services.Interfaces;

namespace MyBlazorApp.Services;

public class ProductsService : IProductsService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonSerializerOptions;


    public ProductsService(HttpClient client)
    {
        this.client = client;
        this.jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Product>?> GetAsync()
    {
        var response = await client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Product>>(content, jsonSerializerOptions);
    }

    public async Task Add(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}