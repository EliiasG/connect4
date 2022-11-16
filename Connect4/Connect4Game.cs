using Connect4.InputMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{

    internal class Connect4Game
    {
        public readonly int Size;
        public Connect4Game(int size) 
        {
            this.Size = size;
        }

        public void Run()
        {
            do
            {
                Board board = new Board(Size);
                Console.WriteLine("How do you want to play?");
                Console.WriteLine("1: Player vs. Player");
                Console.WriteLine("2: Player vs. Computer");
                Console.WriteLine("3: Computer vs. Computer");

                IInputMethod player1 = new PlayerInputMethod(Player.Player1, board);
                IInputMethod player2 = null;

                IntRequest modeRequest = new IntRequest(1, 3);
                switch (modeRequest.Value)
                {
                    case 1:
                        player2 = new PlayerInputMethod(Player.Player2, board);
                        break;
                    case 3:
                        Console.WriteLine("How many games should player 1 simulate per choice?");
                        IntRequest botRequest1 = new IntRequest(1, 1_000_000);
                        player1 = new ComputerInputMethod(Player.Player1, board, botRequest1.Value);
                        goto case 2;
                    case 2:
                        Console.WriteLine("How many games should player 2 simulate per choice?");
                        IntRequest botRequest2 = new IntRequest(1, 1_000_000);
                        player2 = new ComputerInputMethod(Player.Player2, board, botRequest2.Value);
                        break;

                }
                Console.Clear();
                Console.WriteLine("Player 1 starts.");
                while (board.WinningPlayer == Player.None && !board.IsTied)
                {
                    Thread.Sleep(1500);
                    int choice = board.CurrentPlayer == Player.Player1 ? player1.GetChoice() : player2.GetChoice();
                    Console.Clear();
                    Console.WriteLine("Player " + (board.CurrentPlayer == Player.Player1 ? "1" : "2") + " chose:");
                    Console.WriteLine(generateSelectMessage(board, board.GetCol(choice)));
                    board.TakeTurn(choice);
                    Console.WriteLine(board.ToString());
                }
                Console.WriteLine(board.WinningPlayer == Player.None ? "It's a tie!" : (board.WinningPlayer == Player.Player1 ? "Player 1 won!" : "Player 2 won!"));
                Thread.Sleep(5000);
            } while (shouldContinue());
        }

        private bool shouldContinue()
        {
            Console.Clear();
            Console.WriteLine("Do you want to play again? (0 = no, 1 = yes)");
            IntRequest choice = new IntRequest(0, 1);
            return choice.Value == 1;
        }

        private string generateSelectMessage(Board board, int col)
        {
            string res = "";
            for (int i = 0; i < board.Size; i++)
            {
                res += (i == col ? "V" : "-") + (i == board.Size - 1 ? "" : "-");
            }
            return res;
        }
    }
}
