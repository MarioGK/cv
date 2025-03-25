using Blazored.LocalStorage;
using Curriculum;
using Curriculum.Common;
using Curriculum.Common.Interfaces;
using Curriculum.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using ZLogger;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.ClearProviders();
builder.Logging.AddZLoggerConsole();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(_ =>
                                  new HttpClient
                                  {
                                      BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                                  });

builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddSingleton<ILocalizationProvider, WebLocalizationProvider>();
builder.Services.AddSingleton<ICurriculumDataProvider, WebCurriculumDataProvider>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();