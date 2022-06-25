using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using APBD_PRO.Client;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("APBD_PRO.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("APBD_PRO.ServerAPI"));

builder.Services.AddApiAuthorization();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYxOTc2QDMyMzAyZTMxMmUzMGFuTk4wcUZYTXdzUld4SVdmM091TlhvbHR5ZFNjN0dZejh2UXhTZFR2UlE9");
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();

