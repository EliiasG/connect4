using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.InputMethods
{
    internal class PlayerInputMethod : IInputMethod
    {
        public readonly Player Player;
        public readonly Board Board;
        public PlayerInputMethod(Player player, Board board)
        {
            this.Player = player;
            this.Board = board;
        }

        public int GetChoice()
        {
            Console.Clear();
            Console.Write("\n\n");
            Console.WriteLine(Board.ToString());
            Console.WriteLine(generateRowMessage());
            Console.WriteLine("Player " + (Player == Player.Player1 ? "1" : "2") + ", please select one of the above rows.");
            IntRequest choiceRequest = new IntRequest(1, Board.MaxChoice + 1);
            return choiceRequest.Value - 1;
        }

        private string generateRowMessage()
        {
            string res = "";
            int ind = 0;
            for (int i = 0; i < Board.Size; i++)
            {
                if (Board.Tiles[i, 0] == Player.None) res += ++ind;
                else res += "¤";
                res += " ";
            }
            return res;
        }
    }
}
