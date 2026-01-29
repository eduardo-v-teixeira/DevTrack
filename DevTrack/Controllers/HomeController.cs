using Microsoft.AspNetCore.Mvc;

namespace DevTrack.Controllers;

/// <summary>
/// Controller da página inicial e páginas estáticas.
/// Rota padrão: /Home/Index → /
/// </summary>
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
