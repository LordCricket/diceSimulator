public static class Roller
{
    public static List<int> RollDice(Die die, int quantity)
    {
        List<int> rolls = new();

        for (int i = 0; i < quantity; i++)
        {
            rolls.Add(die.Roll());
        }

        return rolls;
    }

    public static int CalculateTotal(List<int> rolls)
    {
        int total = 0;

        for (int i = 0; i < rolls.Count; i++)
        {
            total += rolls[i];
        }

        return total;
    }
}