using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Represents a wizard character.
/// </summary>
public class Wizard : Character
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Wizard"/> class.
    /// </summary>
    /// <param name="name">The wizard's name.</param>
    /// <param name="level">The wizard's initial level.</param>
    /// <param name="maxHealth">The wizard's maximum health points.</param>
    /// <param name="baseAttack">The wizard's base attack value.</param>
    /// <param name="baseDefense">The wizard's base defense value.</param>
    public Wizard(
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
