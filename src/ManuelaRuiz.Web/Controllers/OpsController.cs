using Microsoft.AspNetCore.Mvc;
using ManuelaRuiz.Web.Demo;

namespace ManuelaRuiz.Web.Controllers;

public class OpsController : Controller
{
    private readonly LeadStore _leads;

    public OpsController(LeadStore leads) => _leads = leads;

    [HttpGet("/ops")]
    public IActionResult Index() => View(_leads.All());
}
