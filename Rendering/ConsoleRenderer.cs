public static class Renderer
{
    public static List<string[]> BuildPercentile(int value)
    {
        int tens = value % 100 / 10 * 10;
        int units = value % 10;

        string tensLabel = tens.ToString("D2");

        List<string[]> dice = new();

        dice.Add(BuildKiteDie(tensLabel));
        dice.Add(BuildKiteDie(units.ToString()));

        return dice;
    }

    public static string[] BuildSquareDie(int value)
    {
        string number = value.ToString().PadLeft(4).PadRight(7);

        return new string[]
        {
            "┌───────┐",
            "│       │",
            $"│{number}│",
            "│       │",
            "└───────┘"
        };
    }

    public static string[] BuildTriangleDie(int value)
    {
        string number = value.ToString().PadLeft(4).PadRight(6);

        return new string[]
        {
            "    ╱╲    ",
            "   ╱  ╲   ",
            "  ╱    ╲  ",
            $" ╱{number}╲",
            "╱▁▁▁▁▁▁▁▁╲"
        };
    }

    public static string[] BuildKiteDie(string value)
    {
        string number = value.Length == 1 ? $" {value}  " : $" {value} ";

        return new string[]
        {
            "    ╱╲    ",
            "   ╱  ╲   ",
            "  ╱    ╲  ",
            $"  ╲{number}╱  ",
            "   ╲  ╱",
            "    ╲╱"
        };
    }

    public static string[] BuildPentagonDie(int value)
    {
        string number = value.ToString().PadLeft(3).PadRight(4);

        return new string[]
        {
            "    ╱╲    ",
            "   ╱  ╲   ",
            $"  ╱{number}╲",
            "  ╲    ╱",
            "   ╲__╱"
        };
    }

    public static string[] BuildDice(int sides, int value)
    {
        return sides switch
        {
            4 or 8 or 20 => BuildTriangleDie(value), 
            6 => BuildSquareDie(value),
            10 => BuildKiteDie(value.ToString()),
            12 => BuildPentagonDie(value),
            _ => Array.Empty<string>()
        };
    }

    public static void RenderDice(int sides, List<int> rolls)
    {
        List<string[]> diceArt = new();

        foreach (int number in rolls)
        {
            if (sides == 100)
            {
                diceArt.AddRange(BuildPercentile(number));
            }
            else
            {
                diceArt.Add(BuildDice(sides, number));
            }
        }

        for (int row = 0; row < diceArt[0].Length; row++)
        {
            for (int i = 0; i < diceArt.Count; i++)
            {
                Console.Write(diceArt[i][row].PadRight(10) + "   ");
            }     
            Console.WriteLine();
        }

        if (sides == 100)
        {
            for (int i = 0; i < rolls.Count; i++)
            {
                string label = rolls.Count == 1 ? $"Percentile: {rolls[i]}" : $"Percentile {i + 1}: {rolls[i]}";
                Console.WriteLine (label);
            }
        }

        else
        {
            for (int i = 0; i < diceArt.Count; i++)
            {
                string label = $"Die {i + 1}: {rolls[i]}";
                Console.Write(label.PadRight(11));
            }
        }
        Console.WriteLine();
    }
}