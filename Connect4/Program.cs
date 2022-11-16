using Connect4;

Board b = new Board(7);
while (b.WinningPlayer == Player.None && !b.IsTied)
{
    Console.WriteLine(b);
    Console.WriteLine((b.CurrentPlayer == Player.Player1 ? "Player 1" : "Player 2") + ", make a move");
    string ans = Console.ReadLine() ?? "";
    int choice = Int16.Parse(ans);
    b.TakeTurn(choice);
}
Console.WriteLine(b.IsTied ? "It's a tie!" : b.WinningPlayer == Player.Player1 ? "Player 1 won!" : "Player 2 won!");