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
        string top = "│       │";
        string middle = "│       │";
        string bottom = "│       │";

        switch (value)
        {
            case 1:
            middle = "│   ●   │";
            break;

            case 2:
            top = "│ ●     │";
            bottom = "│     ● │";
            break;

            case 3:
            top = "│ ●     │";
            middle = "│   ●   │";
            bottom = "│     ● │";
            break;

            case 4:
            top = "│ ●   ● │";
            bottom = "│ ●   ● │";
            break;

            case 5:
            top = "│ ●   ● │";
            middle = "│   ●   │";
            bottom = "│ ●   ● │";
            break;

            case 6:
            top = "│ ●   ● │";
            middle = "│ ●   ● │";
            bottom = "│ ●   ● │";
            break;
        }

        return new string[]
        {
            "┌───────┐",
            top,
            middle,
            bottom,
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

        int terminalWidth = Console.WindowWidth;
        int dieWidth = diceArt[0].Max(line => line.Length) + 3;
        int dicePerRow = Math.Max(1, terminalWidth / dieWidth);

        if (sides == 100)
        {
            dicePerRow = Math.Max(1, dicePerRow / 2) * 2;
        }

        for (int start = 0; start < diceArt.Count; start += dicePerRow)
        {
            int end = Math.Min(start + dicePerRow, diceArt.Count);

            for (int row = 0; row < diceArt[0].Length; row++)
            {
                for (int i = start; i < end; i++)
                {
                    Console.Write(diceArt[i][row].PadRight(dieWidth - 3) + "   ");
                }     
                Console.WriteLine();
            }

            if (sides != 100)
            {
                for (int i = start; i < end; i++)
                {
                    string label = $"Die {i + 1}: {rolls[i]}";
                    Console.Write(label.PadRight(dieWidth));
                }
                Console.WriteLine();
            }

            else
            {
                for (int i = start; i < end; i += 2)
                {
                    int rollIndex = i / 2;

                    string label = rolls.Count == 1 ? $"Percentile: {rolls[rollIndex]}" : $"Percentile {rollIndex + 1}: {rolls[rollIndex]}";

                    Console.Write(label.PadRight(dieWidth * 2));
                }
                Console.WriteLine();
            }
        }
    }
}