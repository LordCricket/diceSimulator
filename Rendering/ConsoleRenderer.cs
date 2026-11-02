public static class Renderer
{
    public static string[] RenderD6(int value)
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

    public static void RenderDice(List<int> rolls)
    {
        List<string[]> diceArt = new();

        foreach (int number in rolls)
        {
            diceArt.Add(RenderD6(number));
        }

        for (int row = 0; row < 5; row++)
        {
            for (int i = 0; i < diceArt.Count; i++)
            {
                Console.Write(diceArt[i][row] + "   ");
            }     
            Console.WriteLine();
        } 
        
        for (int i = 0; i < diceArt.Count; i++)
        {
            string label = $"Die {i + 1}: {rolls[i]}";
            Console.Write(label.PadRight(11));
        }
        Console.WriteLine();
    }
}