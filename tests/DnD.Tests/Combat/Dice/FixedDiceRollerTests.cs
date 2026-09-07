using DnD.Combat.Dice;

namespace DnD.Tests.Combat.Dice;

/// <summary>
/// Contains tests for the <see cref="FixedDiceRoller"/> class.
/// </summary>
public class FixedDiceRollerTests
{
    /// <summary>
    /// Verifies that a supported die returns the configured fixed value.
    /// </summary>
    /// <param name="fixedValue">The value configured for the dice roller.</param>
    /// <param name="sides">The number of sides on the die.</param>
    [Theory]
    [InlineData(1, 1)]
    [InlineData(3, 6)]
    [InlineData(20, 20)]
    public void RollWithSupportedDieReturnsFixedValue(int fixedValue, int sides)
    {
        var diceRoller = new FixedDiceRoller(fixedValue);

        int result = diceRoller.Roll(sides);

        Assert.Equal(fixedValue, result);
    }

    /// <summary>
    /// Verifies that a fixed value below one is rejected.
    /// </summary>
    /// <param name="fixedValue">The invalid fixed value.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConstructorWithValueLessThanOneThrowsArgumentOutOfRangeException(
        int fixedValue)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new FixedDiceRoller(fixedValue));
    }

    /// <summary>
    /// Verifies that a die which cannot produce the fixed value is rejected.
    /// </summary>
    /// <param name="fixedValue">The value configured for the dice roller.</param>
    /// <param name="sides">The number of sides on the unsupported die.</param>
    [Theory]
    [InlineData(2, 1)]
    [InlineData(20, 6)]
    public void RollWhenDieCannotProduceFixedValueThrowsArgumentOutOfRangeException(
        int fixedValue,
        int sides)
    {
        var diceRoller = new FixedDiceRoller(fixedValue);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => diceRoller.Roll(sides));
    }
}
