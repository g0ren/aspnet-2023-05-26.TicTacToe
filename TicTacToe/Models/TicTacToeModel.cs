using System.Web.Mvc;

namespace TicTacToe.Models;

public class TicTacToeModel
{
    public string NameX { get; set; }
    public string NameO { get; set; }
    public List<string> Field { get; private set; }

    public int WhoseMove { get; set; }

    List<int> LineSums =>
        new List<int>
        {
            DesymbolForSums(Field[0]) +
            DesymbolForSums(Field[1]) +
            DesymbolForSums(Field[2]),
            DesymbolForSums(Field[3]) +
            DesymbolForSums(Field[4]) +
            DesymbolForSums(Field[5]),
            DesymbolForSums(Field[6]) +
            DesymbolForSums(Field[7]) +
            DesymbolForSums(Field[8]),
            DesymbolForSums(Field[0]) +
            DesymbolForSums(Field[3]) +
            DesymbolForSums(Field[6]),
            DesymbolForSums(Field[1]) +
            DesymbolForSums(Field[4]) +
            DesymbolForSums(Field[7]),
            DesymbolForSums(Field[2]) +
            DesymbolForSums(Field[5]) +
            DesymbolForSums(Field[8]),
            DesymbolForSums(Field[0]) +
            DesymbolForSums(Field[4]) +
            DesymbolForSums(Field[8]),
            DesymbolForSums(Field[2]) +
            DesymbolForSums(Field[4]) +
            DesymbolForSums(Field[6])
        };

    public int Winner
    {
        get
        {
            foreach (var sum in LineSums)
            {
                switch (sum)
                {
                    case (int)Symbols.X * 3:
                        return (int)Symbols.X;
                    case (int)Symbols.O * 3:
                        return (int)Symbols.O;
                }
            }
            return (int)Symbols.Empty;
        }
    }

    public bool Draw
    {
        get
        {
            if (Winner != (int)Symbols.Empty) 
                return false;
            foreach (var sum in LineSums)
            {
                if(sum > (int)Symbols.O * 3)
                    return false;
            }
            return true;
        }
    }
    
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

    int DesymbolForSums(string ch)
    {
        switch (ch)
        {
            case "X":
                return (int)Symbols.X;
            case "0":
                return (int)Symbols.O;
            default:
                return 7;
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


    public TicTacToeModel()
    {
        WhoseMove = GameState.WhoseMove;
        MakeField();
        Deserialize(GameState.Encoded);
    }
}
