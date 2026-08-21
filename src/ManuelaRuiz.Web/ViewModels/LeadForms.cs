using System.ComponentModel.DataAnnotations;

namespace ManuelaRuiz.Web.ViewModels;

public sealed class IntakeLeadForm
{
    [Required] public string Name { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Phone { get; set; } = "";
    public string? Goal { get; set; }
    public string? Language { get; set; }
    public string? Timeline { get; set; }
    public string? Financing { get; set; }
    public string? Credit { get; set; }
    public string? EmploymentYears { get; set; }
    public string? Budget { get; set; }
    public string? Area { get; set; }
    public string? Notes { get; set; }
    public string? Source { get; set; }
    [Required] public string Consent { get; set; } = "";
}

public sealed class SearchLeadForm
{
    [Required] public string Name { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Phone { get; set; } = "";
    [Required] public string Location { get; set; } = "";
    public string? MinPrice { get; set; }
    public string? MaxPrice { get; set; }
    public string? Beds { get; set; }
    public string? Baths { get; set; }
    public string? PropertyType { get; set; }
    public string? Timeline { get; set; }
    public string? Language { get; set; }
    [Required] public string Consent { get; set; } = "";
}

public sealed class QualifyLeadForm
{
    [Required] public string Name { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Phone { get; set; } = "";
    public string? Area { get; set; }
    public string? Credit { get; set; }
    public string? Timeline { get; set; }
    public string? Language { get; set; }
    public string? RangeLow { get; set; }
    public string? RangeHigh { get; set; }
    [Required] public string Consent { get; set; } = "";
}

public sealed class ProgramLeadForm
{
    [Required] public string Name { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Phone { get; set; } = "";
    public string? Program { get; set; }
    public string? Notes { get; set; }
    public string? Language { get; set; }
    [Required] public string Consent { get; set; } = "";
}
