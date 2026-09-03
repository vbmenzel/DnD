using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Represents a rogue character.
/// </summary>
public class Rogue : Character
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Rogue"/> class.
    /// </summary>
    /// <param name="name">The rogue's name.</param>
    /// <param name="level">The rogue's initial level.</param>
    /// <param name="maxHealth">The rogue's maximum health points.</param>
    /// <param name="baseAttack">The rogue's base attack value.</param>
    /// <param name="baseDefense">The rogue's base defense value.</param>
    public Rogue(
        string name,
        int level,
        int maxHealth,
        int baseAttack,
        int baseDefense)
        : base(name, level, maxHealth, baseAttack, baseDefense)
    {
    }

    /// <inheritdoc />
    public override void Attack(IDamageable target)
    {
        int damage = Math.Max(BaseAttack, 0);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!");
    }
}
