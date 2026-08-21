using Microsoft.AspNetCore.Mvc;
using ManuelaRuiz.Web.Demo;
using ManuelaRuiz.Web.ViewModels;

namespace ManuelaRuiz.Web.Controllers;

public class LeadsController : Controller
{
    private readonly LeadStore _leads;

    public LeadsController(LeadStore leads) => _leads = leads;

    [HttpPost("/leads/intake")]
    [ValidateAntiForgeryToken]
    public IActionResult Intake(IntakeLeadForm form)
    {
        var back = LocalBack("/#start");
        if (!ModelState.IsValid)
        {
            TempData["LeadError"] = "intake";
            return Redirect(back);
        }

        _leads.Add(LeadKind.Intake, new Dictionary<string, string?>
        {
            ["name"] = form.Name,
            ["email"] = form.Email,
            ["phone"] = form.Phone,
            ["goal"] = form.Goal,
            ["language"] = form.Language,
            ["timeline"] = form.Timeline,
            ["financing"] = form.Financing,
            ["credit"] = form.Credit,
            ["employmentYears"] = form.EmploymentYears,
            ["budget"] = form.Budget,
            ["area"] = form.Area,
            ["notes"] = form.Notes,
            ["source"] = form.Source,
            ["consent"] = form.Consent
        });

        TempData["LeadOk"] = "intake";
        return Redirect(back);
    }

    [HttpPost("/leads/search")]
    [ValidateAntiForgeryToken]
    public IActionResult Search(SearchLeadForm form)
    {
        var back = LocalBack("/search");
        if (!ModelState.IsValid)
        {
            TempData["LeadError"] = "search";
            return Redirect(back);
        }

        _leads.Add(LeadKind.Search, new Dictionary<string, string?>
        {
            ["name"] = form.Name,
            ["email"] = form.Email,
            ["phone"] = form.Phone,
            ["location"] = form.Location,
            ["minPrice"] = form.MinPrice,
            ["maxPrice"] = form.MaxPrice,
            ["beds"] = form.Beds,
            ["baths"] = form.Baths,
            ["propertyType"] = form.PropertyType,
            ["timeline"] = form.Timeline,
            ["language"] = form.Language,
            ["consent"] = form.Consent
        });

        TempData["LeadOk"] = "search";
        return Redirect(back);
    }

    [HttpPost("/leads/qualify")]
    [ValidateAntiForgeryToken]
    public IActionResult Qualify(QualifyLeadForm form)
    {
        var back = LocalBack("/qualify");
        if (!ModelState.IsValid)
        {
            TempData["LeadError"] = "qualify";
            return Redirect(back);
        }

        _leads.Add(LeadKind.Qualify, new Dictionary<string, string?>
        {
            ["name"] = form.Name,
            ["email"] = form.Email,
            ["phone"] = form.Phone,
            ["area"] = form.Area,
            ["credit"] = form.Credit,
            ["timeline"] = form.Timeline,
            ["language"] = form.Language,
            ["rangeLow"] = form.RangeLow,
            ["rangeHigh"] = form.RangeHigh,
            ["consent"] = form.Consent
        });

        TempData["LeadOk"] = "qualify";
        return Redirect(back);
    }

    [HttpPost("/leads/program")]
    [ValidateAntiForgeryToken]
    public IActionResult Program(IFormCollection form)
    {
        var name = form["name"].ToString();
        var email = form["email"].ToString();
        var phone = form["phone"].ToString();
        var consent = form["consent"].ToString();
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(consent))
        {
            TempData["LeadError"] = "program";
            return Redirect("/programs#program-contact");
        }

        var fields = form
            .Where(kv => !string.Equals(kv.Key, "__RequestVerificationToken", StringComparison.OrdinalIgnoreCase))
            .ToDictionary(kv => kv.Key, kv => (string?)kv.Value.ToString());

        _leads.Add(LeadKind.Program, fields);
        TempData["LeadOk"] = "program";
        return Redirect("/programs#program-contact");
    }

    private string LocalBack(string fallback)
    {
        var referer = Request.Headers.Referer.ToString();
        if (string.IsNullOrWhiteSpace(referer))
        {
            return fallback;
        }

        if (!Uri.TryCreate(referer, UriKind.Absolute, out var uri))
        {
            return fallback;
        }

        var current = $"{Request.Scheme}://{Request.Host}";
        if (!referer.StartsWith(current, StringComparison.OrdinalIgnoreCase))
        {
            return fallback;
        }

        return uri.PathAndQuery;
    }
}
