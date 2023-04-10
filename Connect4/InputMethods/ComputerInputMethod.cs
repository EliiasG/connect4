namespace Connect4.InputMethods
{
    internal class ComputerInputMethod : IInputMethod
    {
        public readonly Player Player;
        public readonly Board Board;
        public readonly int PredictionAmount;
        public ComputerInputMethod(Player player, Board board, int predictionAmount)
        {
            this.Player = player;
            this.Board = board;
            this.PredictionAmount = predictionAmount;
        }
        public int GetChoice()
        {
            Console.Clear();
            Console.Write("\n\n");
            Console.WriteLine(Board.ToString());
            string firstPart = "Player " + (Player == Player.Player1 ? "1" : "2");
            Thread.Sleep(1000);
            Random random = new Random();
            Board sim = new Board(Board);
            int bestInd = 0;
            int bestRes = 0;
            for (int i = 0; i < Board.MaxChoice + 1; i++)
            {
                int res = 0;
                for (int j = 0; j < PredictionAmount; j++)
                {
                    if (simulateGame(sim, Board, i, random)) ++res;
                    Console.Write("\r" + firstPart + " " + (i * PredictionAmount + j) * 100 / (Board.MaxChoice + 1) / PredictionAmount + "%");
                }
                if (res > bestRes)
                {
                    bestRes = res;
                    bestInd = i;
                }
            }
            return bestInd;
        }

        private bool simulateGame(Board sim, Board game, int choice, Random rnd)
        {
            sim.SetState(game);
            while (sim.WinningPlayer == Player.None && !sim.IsTied)
            {
                sim.TakeTurn(choice);
                choice = rnd.Next(sim.MaxChoice + 1);
            }
            return sim.WinningPlayer == Player;
        }
    }
}
