using System.Web.Mvc;

namespace TicTacToe.Models;

public class TicTacToeModel
{
    public string NameX { get; set; }
    public string NameO { get; set; }
    public List<string> Field { get; private set; }
    
    public int WhoseMove { get; set; }

    enum Symbols
    {
        Empty,
        X,
        O
    }

    string Symbol(int code, int cell)
    {
        switch (code)
        {
            case (int)Symbols.X:
                return "X";
            case (int)Symbols.O:
                return "0";
            default:
                return " ";
        }
    }

    int Desymbol(string ch)
    {
        switch (ch)
        {
            case "X":
                return (int)Symbols.X;
            case "0":
                return (int)Symbols.O;
            default:
                return (int)Symbols.Empty;
        }
    }

    public int Serialize()
    {
        int coeff = 1;
        int encode = 0;
        for (int i = 0; i < 9; i++)
        {
            encode += Desymbol(Field[i]) * coeff;
            coeff *= 3;
        }
        return encode;
    }
    
    
    public void Deserialize(int encode)
    {
        for (int i = 0; i < 9; i++)
        {
            Field[i] = Symbol(encode % 3, i);
            encode /= 3;
        }
    }

    void MakeField()
    {
        Field = new List<String>();
        for(int i = 0; i < 9; i++)
        {
            Field.Add("");
        }
    }

    public TicTacToeModel(string nameX, string nameO)
    {
        NameX = nameX;
        NameO = nameO;
        WhoseMove = 1;
        Random random = new Random();
        MakeField();
        for(int i = 0; i < 9; i++)
        {
            Field[i] = Symbol(random.Next((3)), i);
        }
    }

    public TicTacToeModel(string nameX, string nameO, int whoseMove, int encode)
    {
        NameX = nameX;
        NameO = nameO;
        WhoseMove = whoseMove == 1 ? 2 : 1;
        MakeField();
        Deserialize(encode);
    }
}
