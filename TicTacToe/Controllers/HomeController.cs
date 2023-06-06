using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicTacToe.Models;

namespace TicTacToe.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Game([FromForm(Name = "name")] string name, [FromForm(Name = "encode")] int? encode)
    {
        if (encode != null)
        {
            return View(new TicTacToeModel(name, (int)encode) { Name = name });
        }
        return View(new TicTacToeModel(name));
    }

    [HttpPost]
    public IActionResult GameMove([FromForm(Name = "name")] string name, 
        [FromForm(Name = "encode")] int encode, [FromForm(Name = "cell")] int cell)
    {
        return View("Game", new TicTacToeModel(name, encode+ (int)Math.Pow(3,cell)));
    }
    
    public IActionResult GameOver()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}