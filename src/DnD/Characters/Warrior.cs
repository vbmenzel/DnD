using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Represents a warrior character.
/// </summary>
public class Warrior : Character
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Warrior"/> class.
    /// </summary>
    /// <param name="name">The warrior's name.</param>
    /// <param name="level">The warrior's initial level.</param>
    /// <param name="maxHealth">The warrior's maximum health points.</param>
    /// <param name="baseAttack">The warrior's base attack value.</param>
    /// <param name="baseDefense">The warrior's base defense value.</param>
    public Warrior(
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
