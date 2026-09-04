using DnD.Combat.Dice;

namespace DnD.Tests.Combat.Dice;

/// <summary>
/// Contains tests for the <see cref="RandomDiceRoller"/> class.
/// </summary>
public class RandomDiceRollerTests
{
    /// <summary>
    /// Verifies that valid dice rolls remain within the inclusive die range.
    /// </summary>
    /// <param name="sides">The number of sides on the die.</param>
    [Theory]
    [InlineData(1)]
    [InlineData(6)]
    [InlineData(20)]
    public void RollWithValidNumberOfSidesReturnsValueWithinDieRange(int sides)
    {
        var diceRoller = new RandomDiceRoller();

        for (int rollNumber = 0; rollNumber < 100; rollNumber++)
        {
            int result = diceRoller.Roll(sides);

            Assert.InRange(result, 1, sides);
        }
    }

    /// <summary>
    /// Verifies that a die with fewer than one side is rejected.
    /// </summary>
    /// <param name="sides">The invalid number of sides.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void RollWithInvalidNumberOfSidesThrowsArgumentOutOfRangeException(
        int sides)
    {
        var diceRoller = new RandomDiceRoller();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => diceRoller.Roll(sides));
    }
}
