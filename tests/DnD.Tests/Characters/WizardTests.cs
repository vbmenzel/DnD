using DnD.Characters;
using DnD.Combat.Exceptions;

namespace DnD.Tests.Characters;

/// <summary>
/// Contains tests for Wizard mana and spell behavior.
/// </summary>
public class WizardTests
{
    /// <summary>
    /// Verifies that a Wizard starts with maximum mana for its level.
    /// </summary>
    [Fact]
    public void ConstructorInitializesCurrentManaToMaximumMana()
    {
        var wizard = new Wizard("Mira", 1, 22, 8, 6);

        Assert.Equal(15, wizard.MaxMana);
        Assert.Equal(wizard.MaxMana, wizard.CurrentMana);
    }

    /// <summary>
    /// Verifies that casting a spell consumes mana and damages its target.
    /// </summary>
    [Fact]
    public void CastSpellConsumesManaAndDamagesTarget()
    {
        var wizard = new Wizard("Mira", 1, 22, 8, 6);
        var target = new Monster("Goblin", 1, 30, 3, 3);

        wizard.CastSpell(target);

        Assert.Equal(10, wizard.CurrentMana);
        Assert.Equal(17, target.HP);
    }

    /// <summary>
    /// Verifies that casting without enough mana throws the domain exception.
    /// </summary>
    [Fact]
    public void CastSpellWithoutEnoughManaThrowsInsufficientManaException()
    {
        var wizard = new Wizard("Mira", 1, 22, 8, 6);
        var target = new Monster("Ogre", 1, 100, 3, 3);

        wizard.CastSpell(target);
        wizard.CastSpell(target);
        wizard.CastSpell(target);

        Assert.Throws<InsufficientManaException>(
            () => wizard.CastSpell(target));
    }

    /// <summary>
    /// Verifies that mana restoration cannot exceed maximum mana.
    /// </summary>
    [Fact]
    public void RestoreManaExceedingMissingManaClampsAtMaximumMana()
    {
        var wizard = new Wizard("Mira", 1, 22, 8, 6);
        var target = new Monster("Goblin", 1, 30, 3, 3);
        wizard.CastSpell(target);

        wizard.RestoreMana(100);

        Assert.Equal(wizard.MaxMana, wizard.CurrentMana);
    }

    /// <summary>
    /// Verifies that negative mana restoration is rejected.
    /// </summary>
    [Fact]
    public void RestoreManaWithNegativeAmountThrowsArgumentOutOfRangeException()
    {
        var wizard = new Wizard("Mira", 1, 22, 8, 6);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => wizard.RestoreMana(-1));
    }
}
