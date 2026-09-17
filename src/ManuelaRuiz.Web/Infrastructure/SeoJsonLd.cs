using System.Text.Json;

namespace ManuelaRuiz.Web.Infrastructure;

public static class SeoJsonLd
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = null,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public static string AgentGraph(string siteUrl)
    {
        var root = siteUrl.TrimEnd('/');
        var agentId = $"{root}/#agent";
        var orgId = $"{root}/#brokerage";
        var websiteId = $"{root}/#website";

        var graph = new object[]
        {
            new Dictionary<string, object?>
            {
                ["@type"] = "RealEstateAgent",
                ["@id"] = agentId,
                ["name"] = SiteInfo.Name,
                ["image"] = $"{root}/img/manuela-ruiz.jpg",
                ["url"] = root + "/",
                ["telephone"] = SiteInfo.PhoneTel,
                ["email"] = SiteInfo.Email,
                ["priceRange"] = "$$",
                ["description"] =
                    "Bilingual North Carolina Realtor based in Charlotte for first-time buyers, ITIN financing conversations, new construction, investors, sellers and relocating families.",
                ["address"] = new Dictionary<string, object?>
                {
                    ["@type"] = "PostalAddress",
                    ["streetAddress"] = "1800 Central Ave Suite 303",
                    ["addressLocality"] = "Charlotte",
                    ["addressRegion"] = "NC",
                    ["postalCode"] = "28205",
                    ["addressCountry"] = "US"
                },
                ["geo"] = new Dictionary<string, object?>
                {
                    ["@type"] = "GeoCoordinates",
                    ["latitude"] = 35.2271,
                    ["longitude"] = -80.8431
                },
                ["areaServed"] = new object[]
                {
                    new Dictionary<string, object?> { ["@type"] = "City", ["name"] = "Charlotte" },
                    new Dictionary<string, object?> { ["@type"] = "State", ["name"] = "North Carolina" }
                },
                ["knowsLanguage"] = new[] { "Spanish", "English" },
                ["identifier"] = $"North Carolina real estate license {SiteInfo.License}",
                ["worksFor"] = new Dictionary<string, object?> { ["@id"] = orgId },
                ["sameAs"] = new[]
                {
                    SiteInfo.ProfileUrl,
                    SiteInfo.Instagram,
                    SiteInfo.LinkedIn,
                    SiteInfo.Facebook
                }
            },
            new Dictionary<string, object?>
            {
                ["@type"] = "RealEstateAgent",
                ["@id"] = orgId,
                ["name"] = SiteInfo.Brokerage,
                ["url"] = "https://guerra-realty.com/"
            },
            new Dictionary<string, object?>
            {
                ["@type"] = "WebSite",
                ["@id"] = websiteId,
                ["name"] = "Manuela Ruiz Realty",
                ["url"] = root + "/",
                ["inLanguage"] = new[] { "es-US", "en-US" },
                ["publisher"] = new Dictionary<string, object?> { ["@id"] = agentId }
            }
        };

        var doc = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@graph"] = graph
        };
        return JsonSerializer.Serialize(doc, JsonOptions);
    }

    public static string FaqPage(IEnumerable<(string Question, string Answer)> faqs)
    {
        var entities = faqs.Select(f => new Dictionary<string, object?>
        {
            ["@type"] = "Question",
            ["name"] = f.Question,
            ["acceptedAnswer"] = new Dictionary<string, object?>
            {
                ["@type"] = "Answer",
                ["text"] = f.Answer
            }
        }).ToList();

        var doc = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "FAQPage",
            ["mainEntity"] = entities
        };
        return JsonSerializer.Serialize(doc, JsonOptions);
    }

    public static string Breadcrumb(string siteUrl, IEnumerable<(string Name, string Path)> crumbs)
    {
        var root = siteUrl.TrimEnd('/');
        var list = crumbs.Select((c, i) => new Dictionary<string, object?>
        {
            ["@type"] = "ListItem",
            ["position"] = i + 1,
            ["name"] = c.Name,
            ["item"] = c.Path == "/" ? root + "/" : root + c.Path
        }).ToList();

        var doc = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "BreadcrumbList",
            ["itemListElement"] = list
        };
        return JsonSerializer.Serialize(doc, JsonOptions);
    }
}
