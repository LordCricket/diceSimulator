public class Die
{
    public int Sides {get; set;}

    public Die(int sides)
    {
        Sides = sides;
    }

    public int Roll()
    {
        return Random.Shared.Next(1, Sides + 1);
    }
}