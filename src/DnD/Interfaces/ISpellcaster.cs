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
    /// Casts a spell at the specified target.
    /// </summary>
    /// <param name="target">The target affected by the spell.</param>
    void CastSpell(IDamageable target);
}
