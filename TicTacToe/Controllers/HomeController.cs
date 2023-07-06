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
    public IActionResult AddX([FromForm(Name = "nameX")] string? nameX)
    {
        if (!GameState.ConnectedX)
        {
            GameState.NameX = nameX;
            GameState.ConnectedX = true;
        }
        HttpContext.Session.SetInt32("MyPlayer", 1);
        return View("Game", new TicTacToeModel());
    }
    
    [HttpPost]
    public IActionResult AddO([FromForm(Name = "nameO")] string? nameO)
    {
        if (!GameState.ConnectedO)
        {
            GameState.NameO = nameO;
            GameState.ConnectedO = true;
        }
        GameState.NewGame();
        HttpContext.Session.SetInt32("MyPlayer", 2);
        return View("Game", new TicTacToeModel());
    }
    
    [HttpPost]
    public IActionResult NewGame()
    {
        if(GameState.ConnectedX && GameState.ConnectedO)
        {
            GameState.NewGame();
        }
        
        return View("Game", new TicTacToeModel());
    }
    
    [HttpPost]
    public IActionResult Game()
    {
        return View(new TicTacToeModel());
    }

    [HttpPost]
    public IActionResult GameMove(
        [FromForm(Name = "cell")] int cell)
    {
        ModelState.Clear();
        GameState.Encoded += ((int)Math.Pow(3, cell)) * GameState.WhoseMove;
        GameState.WhoseMove = GameState.WhoseMove == 1 ? 2 : 1;
        return View("Game", 
            new TicTacToeModel());
    }
    
    public IActionResult GameOver()
    {
        GameState.ConnectedO = false;
        GameState.ConnectedX = false;
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