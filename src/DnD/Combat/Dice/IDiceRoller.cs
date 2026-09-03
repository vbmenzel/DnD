namespace DnD.Combat.Dice;

/// <summary>
/// Defines a contract for rolling dice.
/// </summary>
public interface IDiceRoller
{
    /// <summary>
    /// Rolls a die withr the specified number of sides.
    /// </summary>
    /// <param name="sides"></param>
    /// <returns>A value between 1 and <paramref name="sides"/></returns>
    int Roll(int sides);
}