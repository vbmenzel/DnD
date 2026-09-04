using DnD.Characters;

namespace DnD.Tests.Characters;

/// <summary>
/// Contains tests for health behavior shared by all characters.
/// </summary>
public class CharacterTests
{
    /// <summary>
    /// Verifies that a character cannot be created without positive maximum health.
    /// </summary>
    /// <param name="maxHP">The invalid maximum health value.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConstructorWithInvalidMaximumHealthThrowsArgumentOutOfRangeException(
        int maxHP)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Warrior("Aric", 1, maxHP, 9, 7));
    }

    /// <summary>
    /// Verifies that damage cannot reduce health below zero.
    /// </summary>
    [Fact]
    public void TakeDamageExceedingCurrentHealthClampsAtZero()
    {
        var character = new Warrior("Aric", 1, 35, 9, 7);

        character.TakeDamage(100);

        Assert.Equal(0, character.HP);
        Assert.True(character.IsDefeated);
    }

    /// <summary>
    /// Verifies that healing cannot increase health above maximum health.
    /// </summary>
    [Fact]
    public void HealExceedingMissingHealthClampsAtMaximumHealth()
    {
        var character = new Warrior("Aric", 1, 35, 9, 7);
        character.TakeDamage(10);

        character.Heal(100);

        Assert.Equal(character.MaxHP, character.HP);
    }

    /// <summary>
    /// Verifies that negative damage is rejected.
    /// </summary>
    [Fact]
    public void TakeDamageWithNegativeAmountThrowsArgumentOutOfRangeException()
    {
        var character = new Warrior("Aric", 1, 35, 9, 7);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => character.TakeDamage(-1));
    }

    /// <summary>
    /// Verifies that negative healing is rejected.
    /// </summary>
    [Fact]
    public void HealWithNegativeAmountThrowsArgumentOutOfRangeException()
    {
        var character = new Warrior("Aric", 1, 35, 9, 7);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => character.Heal(-1));
    }
}
