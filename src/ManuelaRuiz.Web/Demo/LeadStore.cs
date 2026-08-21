using System.Collections.Concurrent;

namespace ManuelaRuiz.Web.Demo;

public sealed class LeadStore
{
    private readonly ConcurrentDictionary<string, LeadRecord> _leads = new();

    public LeadRecord Add(LeadKind kind, IDictionary<string, string?> fields)
    {
        var clean = fields
            .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
            .ToDictionary(kv => kv.Key, kv => kv.Value!.Trim(), StringComparer.OrdinalIgnoreCase);

        clean.TryGetValue("name", out var name);
        clean.TryGetValue("email", out var email);
        clean.TryGetValue("phone", out var phone);
        clean.TryGetValue("language", out var language);
        clean.TryGetValue("timeline", out var timeline);

        var id = $"MR-{DateTime.UtcNow:yyyyMMdd}-{_leads.Count + 1:000}";
        var summary = kind switch
        {
            LeadKind.Search => $"{clean.GetValueOrDefault("location")} · {clean.GetValueOrDefault("minPrice")}-{clean.GetValueOrDefault("maxPrice")} · {clean.GetValueOrDefault("beds")} bd",
            LeadKind.Qualify => $"Range {clean.GetValueOrDefault("rangeLow")}-{clean.GetValueOrDefault("rangeHigh")} · {clean.GetValueOrDefault("area")}",
            LeadKind.Intake => $"{clean.GetValueOrDefault("goal") ?? clean.GetValueOrDefault("source")} · {clean.GetValueOrDefault("area")} · {clean.GetValueOrDefault("budget")}",
            _ => clean.GetValueOrDefault("program") ?? kind.ToString()
        };

        var lead = new LeadRecord
        {
            Id = id,
            Kind = kind,
            CreatedAt = DateTimeOffset.UtcNow,
            Name = name ?? "",
            Email = email ?? "",
            Phone = phone ?? "",
            Language = language ?? "",
            Timeline = timeline ?? "",
            Summary = summary ?? "",
            Fields = clean
        };

        _leads[id] = lead;
        return lead;
    }

    public IReadOnlyList<LeadRecord> All() =>
        _leads.Values.OrderByDescending(l => l.CreatedAt).ToList();
}
