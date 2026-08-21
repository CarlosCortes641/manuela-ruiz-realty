namespace ManuelaRuiz.Web.Demo;

public enum LeadKind
{
    Intake,
    Search,
    Qualify,
    Program
}

public sealed class LeadRecord
{
    public string Id { get; init; } = "";
    public LeadKind Kind { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string Name { get; init; } = "";
    public string Email { get; init; } = "";
    public string Phone { get; init; } = "";
    public string Language { get; init; } = "";
    public string Timeline { get; init; } = "";
    public string Summary { get; init; } = "";
    public Dictionary<string, string> Fields { get; init; } = new();
}
