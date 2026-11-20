using Microsoft.AspNetCore.Mvc;

namespace ClubDeportivo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}