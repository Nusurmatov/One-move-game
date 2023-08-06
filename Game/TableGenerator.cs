using Spectre.Console;
using Spectre.Console.Rendering;

public class TableGenerator
{
    public static void PrintUserHelpTable(string[] moves)
    {
        var table = new Table();
        table.AddColumn(new TableColumn("[darkCyan]↓ PC[/]\\[yellow]User ->[/]").Centered());
        Array.ForEach(moves, move => table.AddColumn(new TableColumn($"[yellow]{move}[/]").Centered()));
        Array.ForEach(moves, move => table.AddRow(GenerateRow(moves, move)));
        table.Border(TableBorder.Ascii);
        AnsiConsole.Write(table);
    }

    private static string[] GenerateRow(string[] moves, string move)
    {
        var rules = Rule.Create(moves);
        var row = new string[moves.Length + 1];
        row[0] = $"[darkCyan]{move}[/]";
        for (int i = 0; i < moves.Length; i++)
        {
            row[i + 1] = rules.DetermineWinner(move, moves[i]);
        }
        return row;
    }
}