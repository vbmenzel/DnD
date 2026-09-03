namespace DnD.Combat.Dice;

/// <summary>
/// Provides a deterministic dice roller that always returns a fixed value.
/// </summary>
/// <remarks>
/// This implementation is useful for tests and demonstrations where predictable dice rolls are required.
/// </remarks>
public class FixedDiceRoller : IDiceRoller
{
    /// <summary>
    /// Gets the value returned by each valid dice roll.
    /// </summary>
    public int FixedValue { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FixedDiceRoller"/> class.
    /// </summary>
    /// <param name="fixedValue">
    /// The fixed value that valid dice rolls should return.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="fixedValue"/> is less than one.
    /// </exception>
    public FixedDiceRoller(int fixedValue)
    {
        if (fixedValue < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(fixedValue),
                "The fixed value must be at least one.");
        }

        FixedValue = fixedValue;
    }

    /// <summary>
    /// Returns the configured fixed value for a die with the specified number
    /// of sides.
    /// </summary>
    /// <param name="sides">The number of sides on the die.</param>
    /// <returns>The value specified by <see cref="FixedValue"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the die cannot produce <see cref="FixedValue"/>.
    /// </exception>
    public int Roll(int sides)
    {
        if (sides < FixedValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sides),
                "The die must be able to produce the fixed value.");
        }

        return FixedValue;
    }
}