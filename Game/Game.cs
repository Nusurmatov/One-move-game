using Spectre.Console;

public class Game
{
    public Rule Rules { get; set; }

    private Game(Rule rules) => this.Rules = rules;

    public static Game? Create(Rule? rules) => (rules is not null) ? new Game(rules) : null;

    public void Start()
    {
        var hmacKey = HmacGenerator.GeneratRandomKey();
        var randomMove = this.Rules.RandomMove;
        var hmac = HmacGenerator.GenerateHmac(hmacKey, randomMove);
        AnsiConsole.MarkupLine($"HMAC: [green]{hmac}[/]");
        PrintMoves(this.Rules.Moves);
        var playerMove = AcceptPlayerMove();
        PrintWinner(hmacKey, randomMove, playerMove);
    }

    private void PrintWinner(string hmacKey, string randomMove, string playerMove)
    {
        AnsiConsole.MarkupLine($"Your move: [yellow]{playerMove}[/]");
        AnsiConsole.MarkupLine($"Computer move: [yellow]{randomMove}[/]");
        var winner = this.Rules.DetermineWinner(playerMove, randomMove);
        AnsiConsole.MarkupLine(winner.Contains("Win") ? "[bold green]You won![/]" : "[bold red]You lost![/]");
        AnsiConsole.MarkupLine($"HMAC key: [green]{hmacKey}[/]");
    }

    private string AcceptPlayerMove()
    {
        string? playerMove = null;
        while (playerMove is null)
        {
            playerMove = AnsiConsole.Ask<string>("Enter your move: ");
            playerMove = CheckMove(playerMove);
        }
        return playerMove;
    }

    private string? CheckMove(string move)
    {
        if (move == "?")
        {
            TableGenerator.PrintUserHelpTable(this.Rules.Moves);
            return null;
        }
        else if (move == "0")
        {
            AnsiConsole.MarkupLine("Bye!");
            Environment.Exit(0);
        }
        else if (!this.Rules.Moves.Contains(move))
        {
            AnsiConsole.MarkupLine($"[red]Invalid move: {move}[/]");
            return null;
        }
        return move;
    }

    private void PrintMoves(string[] moves)
    {
        AnsiConsole.MarkupLine("Available moves:");
        int i = 0;
        Array.ForEach(moves, move => AnsiConsole.MarkupLine($"[yellow]{++i}[/] - [green]{move}[/]"));
        AnsiConsole.MarkupLine("[yellow]0[/] - [magenta]exit[/]");
        AnsiConsole.MarkupLine("[yellow]?[/] - [magenta]help[/]");
    }
}