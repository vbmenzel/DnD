namespace DnD.Combat.Dice;

public class RandomDiceRoller : IDiceRoller
{
    public int Roll(int sides)
    {
        if (sides < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sides),
                "A die must have at least one side.");
        }

        return Random.Shared.Next(1, sides + 1);
    }
}