using MyBlazorApp.Models;

namespace MyBlazorApp.Services.Interfaces;

public interface ICategoriesService
{
    Task<List<Category>?> GetAsync();
}