using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicTacToe.Models;

namespace TicTacToe.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, Game game)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ToLobby([FromForm(Name = "name")]string myName)
    {
        HttpContext.Session.SetString("MyName", myName);
        return View("Lobby");
    }
    
    [HttpPost]
    public IActionResult Game(
        [FromForm(Name = "currentGameNumber")] int currentGameNumber)
    {
        return View(new TicTacToeModel(currentGameNumber));
    }

    [HttpPost]
    public IActionResult GameMove(
        [FromForm(Name = "currentGameNumber")] int currentGameNumber,
        [FromForm(Name = "cell")] int cell)
    {
        ModelState.Clear(); 
        var model = new TicTacToeModel(currentGameNumber);
        model.Game.Encoded += ((int)Math.Pow(3, cell)) * model.Game.WhoseTurnAsInt;
        model.Game.isTurnOfX = !model.Game.isTurnOfX;
        return View("Game", 
            model);
    }
    
    [HttpPost]
    public IActionResult AddX([FromForm(Name = "name")] string? name,
        [FromForm(Name = "gameNumber")] int number)
    {
        Lobby.Games[number].ConnectedX = true;
        Lobby.Games[number].NameX = name;
        HttpContext.Session.SetInt32("MyPlayer", 1);
        return View("Game", new TicTacToeModel(number));
    }
    
    [HttpPost]
    public IActionResult AddO([FromForm(Name = "name")] string? name,
        [FromForm(Name = "gameNumber")] int number)
    {
        Lobby.Games[number].ConnectedO = true;
        Lobby.Games[number].NameO = name;
        HttpContext.Session.SetInt32("MyPlayer", 2);
        return View("Game", new TicTacToeModel(number));
    }
    
    [HttpPost]
    public IActionResult AddSpectator([FromForm(Name = "gameNumber")] int number)
    {
        HttpContext.Session.SetInt32("MyPlayer", 0);
        return View("Game", new TicTacToeModel(number));
    }
    
    [HttpPost]
    public IActionResult NewGame(
        [FromForm(Name = "currentGameNumber")] int currentGameNumber)
    {
        ModelState.Clear(); 
        var model = new TicTacToeModel(currentGameNumber);
        if(model.Game.ConnectedX && model.Game.ConnectedO)
        {
            model.Game.NewGame();
        }
        
        return View("Game", model);
    }
    
    [HttpPost]
    public IActionResult GameOver(
        [FromForm(Name = "currentGameNumber")] int currentGameNumber)
    {
        Lobby.Games[currentGameNumber].ConnectedO = false;
        Lobby.Games[currentGameNumber].ConnectedX = false;
        Lobby.Games[currentGameNumber].NewGame();
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