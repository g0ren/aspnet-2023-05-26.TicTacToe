namespace TicTacToe.Models;

public static class GameState
{
    public static readonly int Max = (int)Math.Pow(3, 9);

    public static string? NameX { get; set; }

    public static string? NameO { get; set; }

    public static int WhoseMove { get; set; }
    public static int Encoded { get; set; }

    public static bool ConnectedX { get; set; }

    public static bool ConnectedO { get; set; }

    public static void NewGame()
    {
        Encoded = new Random().Next(Max);
        WhoseMove = 1;
    }
    
}