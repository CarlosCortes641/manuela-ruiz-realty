using System.Globalization;
using ManuelaRuiz.Web.Demo;
using ManuelaRuiz.Web.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<LeadStore>();
builder.Services.AddScoped<SiteLanguage>();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.Use(async (context, next) =>
{
    var wantEn = string.Equals(context.Request.Cookies[SiteLanguage.CookieName], "en", StringComparison.OrdinalIgnoreCase);
    CultureInfo culture;
    try
    {
        culture = CultureInfo.GetCultureInfo(wantEn ? "en-US" : "es-US");
    }
    catch (CultureNotFoundException)
    {
        culture = CultureInfo.GetCultureInfo(wantEn ? "en" : "es");
    }

    CultureInfo.CurrentCulture = culture;
    CultureInfo.CurrentUICulture = culture;
    await next();
});
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
