using Spectre.Console;

public class Rule
{
    private static string _errorMessage = "[red]Invalid moves![/]";
    public string[] Moves { get; }
    public string RandomMove => this.Moves[new Random().Next(this.Moves.Length)];

    private Rule(string[] moves)
    {
        this.Moves = moves;
    }

    public static Rule? Create(string[] moves)
    {
        if (ValidateMoves(moves))
        {
            return new Rule(moves);
        }
        else
        {
            AnsiConsole.Markup(_errorMessage);
            return null;
        }
    }

    public string DetermineWinner(string move1, string move2) 
    {
        if (move1 == move2)
        {
            return "[blue]Draw[/]";
        }
        else
        {
            int move1Index = Array.IndexOf(this.Moves, move1);
            int move2Index = Array.IndexOf(this.Moves, move2);
            return DetermineWinner(move1Index, move2Index) == move1Index ? "[green]Win[/]": "[red]Lose[/]";
        }
    }

    private int DetermineWinner(int move1Index, int move2Index)
    {
        int halfMovesCount = this.Moves.Length / 2;
        int distance = move1Index - move2Index;
        if (distance < 0)
        {
            distance += this.Moves.Length;
        }
        return distance <= halfMovesCount ? move1Index : move2Index;
    }

    private static bool ValidateMoves(string[] moves)
        => ValidateMovesLength(moves) && ValidateMovesUniqueness(moves);

    private static bool ValidateMovesLength(string[] moves)
    {
        bool isValid = moves.Length >= 3 && moves.Length % 2 == 1;
        if (!isValid)
        {
            _errorMessage += " The number of moves must be [red]odd[/] and [red]greater than or equal to 3[/].";
        }
        return isValid;
    }
    private static bool ValidateMovesUniqueness(string[] moves)
    {   
        bool isValid = moves.Distinct().Count() == moves.Length;
        if (!isValid)
        {
            _errorMessage += " The moves must be [red]unique[/].";
        }
        return isValid;
    }
}