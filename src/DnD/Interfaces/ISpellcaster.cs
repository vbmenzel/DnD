using DnD.Combat.Exceptions;

namespace DnD.Interfaces;

/// <summary>
/// Defines the contract for characters that can cast spells.
/// </summary>
public interface ISpellcaster
{
    /// <summary>
    /// Gets the spellcaster's current mana points.
    /// </summary>
    int CurrentMana { get; }

    /// <summary>
    /// Gets the spellcaster's maximum mana points.
    /// </summary>
    int MaxMana { get; }

    /// <summary>
    /// Casts a spell at the specified target.
    /// </summary>
    /// <param name="target">The target affected by the spell.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InsufficientManaException">
    /// Thrown when the spellcaster does not have enough mana.
    /// </exception>
    void CastSpell(IDamageable target);

    /// <summary>
    /// Restores mana without exceeding the spellcaster's maximum mana.
    /// </summary>
    /// <param name="amount">The amount of mana to restore.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="amount"/> is negative.
    /// </exception>
    void RestoreMana(int amount);
}
