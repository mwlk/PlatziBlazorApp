using MyBlazorApp.Models;

namespace MyBlazorApp.Services.Interfaces;

public interface IProductsService
{
    Task<List<Product>?> GetAsync();
    Task Add(Product product);
    Task Delete(int productId);
}