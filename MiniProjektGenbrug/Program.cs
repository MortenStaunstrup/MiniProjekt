using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiniProjektGenbrug;
using MiniProjektGenbrug.Services;
using MiniProjektGenbrug.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Registrer din ProductService
builder.Services.AddSingleton<IProductService, ProductServiceClient>();

await builder.Build().RunAsync();