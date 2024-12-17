using System.Reflection;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorApp;
using MyBlazorApp.Services;
using MyBlazorApp.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration.GetValue<string>("apiUrl");

builder.Services.AddBlazoredToast();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl!) });

RegisterScopedServices(builder.Services, typeof(Program).Assembly);

await builder.Build().RunAsync();

static void RegisterScopedServices(IServiceCollection services, Assembly assembly)
{
    var types = assembly.GetTypes();

    var serviceRegiter = types
        .Where(p => p.IsClass && !p.IsAbstract)
        .SelectMany(imp => imp.GetInterfaces(),
            (imp, interfaceType) => new { Interface = interfaceType, Implementation = imp })
            .Where(r => r.Interface.IsPublic && !r.Interface.IsGenericType)
            .ToList();

    foreach (var item in serviceRegiter)
    {
        services.AddScoped(item.Interface, item.Implementation);
    }
}
