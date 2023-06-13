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
    public IActionResult Game([FromForm(Name = "nameX")] string nameX, 
        [FromForm(Name = "nameO")] string nameO,
        [FromForm(Name = "whoseMove")] int whoseMove,
        [FromForm(Name = "encode")] int? encode)
    {
        return View(encode != null ? 
            new TicTacToeModel(nameX, nameO, whoseMove, (int)encode) : 
            new TicTacToeModel(nameX, nameO));
    }

    [HttpPost]
    public IActionResult GameMove([FromForm(Name = "nameX")] string nameX,
        [FromForm(Name = "nameO")] string nameO,
        [FromForm(Name = "whoseMove")] int whoseMove,
        [FromForm(Name = "encode")] int encode,
        [FromForm(Name = "cell")] int cell)
    {
        ModelState.Clear();
        return View("Game", 
            new TicTacToeModel(nameX, 
                nameO, 
                whoseMove,
                encode + ((int)Math.Pow(3, cell)) * whoseMove));
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