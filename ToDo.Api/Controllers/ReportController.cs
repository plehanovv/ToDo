using Microsoft.AspNetCore.Mvc;

namespace ToDo.Api.Controllers;

public class ReportController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}