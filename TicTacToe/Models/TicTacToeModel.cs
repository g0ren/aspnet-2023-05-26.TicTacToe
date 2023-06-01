namespace TicTacToe.Models;

public class TicTacToeModel
{
    public List<char> Field { get; }
        public TicTacToeModel()
        {
            Random random = new Random();
            Field = new List<char>();
            for(int i = 0; i < 9; i++)
            {
                int val = random.Next(3);
                if (val == 0)
                {
                    Field.Add(' '); 
                }
                else if (val == 1)
                {
                    Field.Add('X');
                }
                else
                {
                    Field.Add('0');
                }
            }
        }
    }
