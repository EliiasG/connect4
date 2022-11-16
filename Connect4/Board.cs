using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    enum Player
    {
        None,
        Player1,
        Player2,
    }
    class Board
    {
        public readonly Player[,] Tiles;
        public readonly int Size;
        public int Round = 0;

        private readonly int[][,] _winConditions;

        public int MaxChoice
        {
            get
            {
                int res = -1;
                for (int i = 0; i < Size; i++)
                {
                    if (Tiles[i, 0] == Player.None)
                    {
                        res++;
                    }
                }
                return res;
            }
        }

        public Player WinningPlayer
        {
            get
            {
                foreach (int[,] condition in _winConditions)
                {
                    bool player1won = true;
                    bool player2won = true;
                    for (int i = 0; i < 4; i++)
                    {
                        Player tile = Tiles[condition[i, 0], condition[i, 1]];
                        if (tile != Player.Player1) player1won = false;
                        if (tile != Player.Player2) player2won = false;
                    }
                    if (player1won) return Player.Player1;
                    if (player2won) return Player.Player2;
                }
                return Player.None;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return Round % 2 == 0 ? Player.Player1 : Player.Player2;
            }
        }

        public bool IsTied
        {
            get
            {
                return MaxChoice == 0;
            }
        }

        public Board(int size)
        {
            this.Size = size;
            this._winConditions = calculateWinConditions();
            Tiles = new Player[size, size];
        }
        
        

        private void updateGravity()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 1; j < Size; j++)
                {
                    if (Tiles[i, j] == Player.None)
                    {
                        Tiles[i, j] = Tiles[i, j-1];
                        Tiles[i, j - 1] = Player.None;
                    }
                }
            }
        }

        public void TakeTurn(int col)
        {
            int pos = -1;
            while (col >= 0)
            {
                pos++;
                if (pos >= Size) throw new ArgumentException("row must be <= GetMax()");
                if (Tiles[pos, 0] == Player.None) --col;
            }
            Tiles[pos, 0] = CurrentPlayer;
            updateGravity();
            ++Round;
        }

        public void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Tiles[i, j] = Player.None;
                }
            }
        }

        public void PrintWinConditions()
        {
            Board printer = new Board(Size);
            foreach (int[,] c in _winConditions)
            {
                printer.Clear();
                for (int i = 0; i < 4; i++)
                {
                    printer.Tiles[c[i, 0], c[i, 1]] = Player.Player1;
                }
                Console.WriteLine(printer.ToString());
            }
        }

        private int[][,] calculateWinConditions()
        {
            List<int[,]> conditions = new List<int[,]>();
            foreach (int[,] condition in new int[][,]
            {
                new int[,] {{0, 0}, {1, 0}, {2, 0}, {3, 0}},
                new int[,] {{0, 0}, {0, 1}, {0, 2}, {0, 3}},
                new int[,] {{0, 0}, {1, 1}, {2, 2}, {3, 3}},
                new int[,] {{0, 3}, {1, 2}, {2, 1}, {3, 0}},
            })
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        int[,] moved = movedCondition(i, j, condition);
                        if(isWinConditionValid(moved))
                        {
                            conditions.Add(moved);
                        }
                    }
                }
            }
            return conditions.ToArray();
        }

        private int[,] movedCondition(int x, int y, int[,] condition)
        {
            int[,] res = new int[4, 2];
            for (int i = 0; i < 4; i++)
            {
                res[i, 0] = condition[i , 0] + x;
                res[i, 1] = condition[i , 1] + y;
            }
            return res;
        }

        private bool isWinConditionValid(int[,] condition)
        {
            for (int i = 0; i < 4; i++)
            {
                if (condition[i, 0] >= Size) return false;
                if (condition[i, 1] >= Size) return false;
            }
            return true;
        }

        public Board(Board from) 
        {
            this.Size = from.Size;
            this._winConditions = from._winConditions;
            Tiles = new Player[Size, Size];
            SetState(from);
        }

        public void SetState(Board board)
        {
            if (board.Size != Size) throw new ArgumentException("Size is final, when copying a board the sizes must be identical!");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Tiles[i, j] = board.Tiles[i, j];
                }
            }
            Round = board.Round;
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    switch (Tiles[j, i])
                    {
                        case Player.None:
                            res += "_";
                            break;
                        case Player.Player1:
                            res += "X";
                            break;
                        case Player.Player2:
                            res += "0";
                            break;
                    }
                    res += " ";
                }
                res += "\n";
            }
            return res;
        }
    }
}