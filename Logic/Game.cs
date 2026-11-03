using System.Diagnostics.Contracts;

public class Game
{
    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("[1] - Roll Dice");
            Console.WriteLine("[2] - Quit");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                int sides = SelectDie();
                if (sides == 0)
                {
                    Console.WriteLine("Invalid Die Selection!");
                    Thread.Sleep(250);
                    break;
                }

                int quantity = SelectQuantity();
                if (quantity == 0)
                {
                    Console.WriteLine("Invalid quantity selection!");
                    Thread.Sleep(250);
                    break;
                }

                Die die = new Die(sides);
                List<int> rolls = Roller.RollDice(die, quantity);
                int total = Roller.CalculateTotal(rolls);

                Console.Clear();
                Console.WriteLine($"Selected D{sides}");
                Console.WriteLine($"Quantity: {quantity}");
                Console.WriteLine();
                Console.WriteLine("Rolling...");
                Thread.Sleep(250);
                Console.Write(".");
                Thread.Sleep(250);
                Console.Write(".");
                Thread.Sleep(250);
                Console.WriteLine(".");
                Thread.Sleep(250);
                Console.Clear();

                Renderer.RenderDice(sides, rolls);
                Console.WriteLine();
                if (sides == 100)
                {
                    int max = rolls.Count * 100;
                    double percentage = (double)total / max * 100;

                    Console.WriteLine($"Combined: {total}/{max}");
                    Console.WriteLine($"Average Percentile: {percentage:F2}%");
                }
                else
                {
                    Console.WriteLine($"Total: {total}");
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey(true);
                Console.Clear();

                break;

                case "2":
                running = false;
                break;

                default:
                Console.WriteLine("Invalid");
                break;
            }
        }
    }

    private int SelectDie()
    {
        Console.Clear();
        Console.WriteLine("[1] - D4");
        Console.WriteLine("[2] - D6");
        Console.WriteLine("[3] - D8");
        Console.WriteLine("[4] - D10");
        Console.WriteLine("[5] - D12");
        Console.WriteLine("[6] - D20");
        Console.WriteLine("[7] - D100");
        Console.Write("> ");

        string? input = Console.ReadLine();

        return input switch
        {
            "1" => 4,
            "2" => 6,
            "3" => 8,
            "4" => 10,
            "5" => 12,
            "6" => 20,
            "7" => 100,
            _ => 0
        };
    }

    private int SelectQuantity()
    {
        Console.Clear();
        Console.WriteLine("Enter Dice Quantity");
        Console.Write("> ");
        string? input = Console.ReadLine();

        bool success = int.TryParse(input, out int quantity);

        if (success && quantity > 0 && quantity < 10)
        {
            return quantity;
        }
        else
        {
            return 0;
        }
    }
}