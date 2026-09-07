using DnD.Interfaces;

namespace DnD.Combat.Dice;

/// <summary>
/// Provides dice rolls using a random number generator.
/// </summary>
public class RandomDiceRoller : IDiceRoller
{
    /// <summary>
    /// Rolls a die with the specified number of sides.
    /// </summary>
    /// <param name="sides">The number of sides on the die.</param>
    /// <returns>
    /// A randomly generated value between one and
    /// <paramref name="sides"/>, inclusive.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="sides"/> is less than one.
    /// </exception>
    public int Roll(int sides)
    {
        if (sides < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sides),
                "A die must have at least one side.");
        }

        return Random.Shared.Next(sides) + 1;
    }
}
