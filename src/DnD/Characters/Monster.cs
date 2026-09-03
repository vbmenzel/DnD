using DnD.Combat.Exceptions;
using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Represents a hostile character that can participate in combat.
/// </summary>
public class Monster : Character
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Monster"/> class.
    /// </summary>
    /// <param name="name">The monster's name.</param>
    /// <param name="level">The monster's initial level.</param>
    /// <param name="maxHealth">The monster's maximum health points.</param>
    /// <param name="baseAttack">The monster's base attack value.</param>
    /// <param name="baseDefense">The monster's base defense value.</param>
    public Monster(
        string name,
        int level,
        int maxHealth,
        int baseAttack,
        int baseDefense)
        : base(name, level, maxHealth, baseAttack, baseDefense)
    {
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="CharacterIsDefeatedException">
    /// Thrown when the monster has been defeated.
    /// </exception>
    public override void Attack(IDamageable target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (IsDefeated)
        {
            throw new CharacterIsDefeatedException(
                $"{Name} cannot attack because it has been defeated.");
        }

        int damage = Math.Max(BaseAttack, 0);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!");
    }
}
