namespace DnD.Interfaces;

/// <summary>
/// Defines a contract for rolling dice.
/// </summary>
public interface IDiceRoller
{
    /// <summary>
    /// Rolls a die with the specified number of sides.
    /// </summary>
    /// <param name="sides">The number of sides on the die.</param>
    /// <returns>A value between 1 and <paramref name="sides"/>.</returns>
    int Roll(int sides);
}
