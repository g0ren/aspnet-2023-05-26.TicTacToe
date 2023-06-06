using System.Web.Mvc;

namespace TicTacToe.Models;

public class TicTacToeModel
{
    public string Name { get; set; }
    public List<string> Field { get; private set; }

    enum Symbols
    {
        Empty,
        X,
        O
    }

    string ButtonCode(int cellNo)
    {
        return "<form method=\"post\" action=\"/Home/GameMove\">" +
               $"<input type=\"hidden\" id=\"encode\" name=\"encode\" value=\"{Serialize()}\"/>" +
               $"<input type=\"hidden\" id=\"cell\" name=\"cell\" value=\"{cellNo}\"/>" +
               $"<input type=\"hidden\" id=\"name\" name=\"name\" value=\"{Name}\"/>" +
               "<button class=\"cell-button\" type=\"submit\"> ? </button>" +
               "</form>";
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

    void PutButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Field[i] == " ")
            {
                Field[i] = ButtonCode(i);
            }
        }
    }
    
    public TicTacToeModel(string name)
    {
        Name = name;
        Random random = new Random();
        MakeField();
        for(int i = 0; i < 9; i++)
        {
            Field[i] = Symbol(random.Next((3)), i);
        }
        PutButtons();
    }

    public TicTacToeModel(string name, int encode)
    {
        Name = name;
        MakeField();
        Deserialize(encode);
        PutButtons();
    }
}
