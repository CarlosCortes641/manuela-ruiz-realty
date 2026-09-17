using System.Text;
using ManuelaRuiz.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ManuelaRuiz.Web.Controllers;

public class SeoController : Controller
{
    private static readonly (string Path, string Priority, string ChangeFreq)[] PublicPages =
    [
        ("/", "1.0", "weekly"),
        ("/search", "0.9", "weekly"),
        ("/qualify", "0.9", "weekly"),
        ("/buyers", "0.8", "monthly"),
        ("/sellers", "0.8", "monthly"),
        ("/relocation", "0.7", "monthly"),
        ("/areas", "0.7", "monthly"),
        ("/programs", "0.8", "monthly"),
        ("/partners", "0.6", "monthly"),
        ("/sold", "0.8", "weekly"),
        ("/first-time-buyers", "0.7", "monthly"),
        ("/investors", "0.7", "monthly"),
        ("/new-construction", "0.7", "monthly"),
        ("/resources", "0.6", "monthly")
    ];

    [HttpGet("/sitemap.xml")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public IActionResult Sitemap()
    {
        var baseUrl = PublicUrl.Base(Request);
        var lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var xml = new StringBuilder();
        xml.AppendLine("""<?xml version="1.0" encoding="UTF-8"?>""");
        xml.AppendLine("""<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">""");
        foreach (var (path, priority, changeFreq) in PublicPages)
        {
            var loc = path == "/" ? baseUrl + "/" : baseUrl + path;
            xml.AppendLine("  <url>");
            xml.AppendLine($"    <loc>{System.Security.SecurityElement.Escape(loc)}</loc>");
            xml.AppendLine($"    <lastmod>{lastmod}</lastmod>");
            xml.AppendLine($"    <changefreq>{changeFreq}</changefreq>");
            xml.AppendLine($"    <priority>{priority}</priority>");
            xml.AppendLine("  </url>");
        }

        xml.AppendLine("</urlset>");
        return Content(xml.ToString(), "application/xml", Encoding.UTF8);
    }

    [HttpGet("/robots.txt")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public IActionResult Robots()
    {
        var baseUrl = PublicUrl.Base(Request);
        var body =
            $"""
             User-agent: *
             Allow: /
             Disallow: /ops
             Disallow: /lang/
             Disallow: /leads/

             Sitemap: {baseUrl}/sitemap.xml
             """;
        return Content(body, "text/plain", Encoding.UTF8);
    }
}
