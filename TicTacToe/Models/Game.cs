namespace TicTacToe.Models;

public class Game
{
    private static readonly int Max = (int)Math.Pow(3, 9);
    public int Id { get; set; }
    public string? NameX { get; set; }

    public string? NameO { get; set; }

    public bool isTurnOfX { get; set; }

    public int WhoseTurnAsInt => isTurnOfX ? 1 : 2;

    public int Encoded { get; set; }

    public bool ConnectedX { get; set; }

    public bool ConnectedO { get; set; }

    public void NewGame()
    {
        Encoded = new Random().Next(Max);
        isTurnOfX = true;
    }

    public Game()
    {
        NewGame();
    }
    
}