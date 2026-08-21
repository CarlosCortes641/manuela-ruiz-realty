using Microsoft.AspNetCore.Mvc;
using ManuelaRuiz.Web.Infrastructure;

namespace ManuelaRuiz.Web.Controllers;

public class LanguageController : Controller
{
    [HttpGet("/lang/{code}")]
    public IActionResult Set(string code, string? returnUrl)
    {
        var lang = string.Equals(code, "en", StringComparison.OrdinalIgnoreCase) ? "en" : "es";
        Response.Cookies.Append(SiteLanguage.CookieName, lang, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            IsEssential = true,
            HttpOnly = false,
            SameSite = SameSiteMode.Lax,
            Path = "/"
        });

        if (string.IsNullOrWhiteSpace(returnUrl) || !Url.IsLocalUrl(returnUrl))
        {
            returnUrl = "/";
        }

        return LocalRedirect(returnUrl);
    }
}
