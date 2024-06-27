using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleCurrencyApp.Gui;
using SimpleCurrencyApp.classes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Create currency loader
builder.Services.AddScoped(sp => new CurrencyLoader(builder.Configuration["BaseApi"] ?? string.Empty, builder.Configuration["ApiKey"] ?? string.Empty));

await builder.Build().RunAsync();
