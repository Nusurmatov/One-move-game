public class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        var rules = Rule.Create(moves: args);
        var game = Game.Create(rules);
        game?.Start();
    }
}