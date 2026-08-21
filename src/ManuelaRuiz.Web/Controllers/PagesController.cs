using Microsoft.AspNetCore.Mvc;

namespace ManuelaRuiz.Web.Controllers;

[Route("search")]
public class SearchController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("qualify")]
public class QualifyController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("buyers")]
public class BuyersController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("sellers")]
public class SellersController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("relocation")]
public class RelocationController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("areas")]
public class AreasController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("programs")]
public class ProgramsController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("partners")]
public class PartnersController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("sold")]
public class SoldController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("first-time-buyers")]
public class FirstTimeBuyersController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("resources")]
public class ResourcesController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("investors")]
public class InvestorsController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}

[Route("new-construction")]
public class NewConstructionController : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();
}
