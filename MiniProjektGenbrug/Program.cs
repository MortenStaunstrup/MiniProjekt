using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MiniProjektGenbrug;
using MiniProjektGenbrug.Services;
using MiniProjektGenbrug.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<IProductService, ProductServiceServer>();
builder.Services.AddSingleton<IRoomService, RoomServiceServer>();
builder.Services.AddScoped<IUserService, UserServiceClient>();
builder.Services.AddSingleton(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IProductService>(sp =>

{
    var http = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5189/") // brug den port din API kører på
    };
    
    var storage = sp.GetRequiredService<ILocalStorageService>();
    
    return new ProductServiceClient(http, storage);
});

builder.Services.AddScoped<IUserService>(sp =>
{
    var http = new HttpClient { BaseAddress = new Uri("http://localhost:5189/") };
    var storage = sp.GetRequiredService<ILocalStorageService>();
    return new UserServiceClient(http, storage);
});


await builder.Build().RunAsync();