using Blazored.LocalStorage;
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


builder.Services.AddBlazoredLocalStorage();

// Registrer din ProductService
builder.Services.AddSingleton<IProductService, ProductServiceClient>();
builder.Services.AddSingleton<IRoomService, RoomServiceClient>();
builder.Services.AddSingleton<IUserService, UserServiceClient>();

await builder.Build().RunAsync();