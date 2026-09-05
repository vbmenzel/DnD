using System;
using DnD.Combat.Actions;
using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Summary description for Rogue
/// </summary>
public class Rogue : Character
{
    public Rogue(string name, int level, int maxHP, int baseAttack, int baseDefense) : base(name, level, maxHP, baseAttack, baseDefense)
    {

    }

    /// <summary>
    /// Performs a quick two-part attack with level-based follow-up damage.
    /// </summary>
    /// <param name="target">The target receiving both strikes.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    public override void Attack(IDamageable target)
    {
        ArgumentNullException.ThrowIfNull(target);

        int firstStrikeDamage = Math.Max(BaseAttack + DamageBonus, 0);
        int followUpDamage = Math.Max(Level, 0);
        int totalDamage = firstStrikeDamage + followUpDamage;

        target.TakeDamage(firstStrikeDamage);
        target.TakeDamage(followUpDamage);

        Console.WriteLine(
            $"{Name} strikes {target} twice for {totalDamage} total damage!");
    }

    /// <inheritdoc />
    protected override IReadOnlyList<CombatAction> GetClassCombatActions()
    {
        return
        [
            new CombatAction(
                "Quick attack",
                CombatTargetType.Enemy,
                true,
                target => Attack(target)),
            new CombatAction(
                "Sneak attack",
                CombatTargetType.Enemy,
                true,
                SneakAttack,
                -2),
        ];
    }

    /// <summary>
    /// Performs a stronger surprise attack with a reduced chance to hit.
    /// </summary>
    /// <param name="target">The character receiving the attack.</param>
    private void SneakAttack(Character target)
    {
        int damage = Math.Max(BaseAttack + DamageBonus + (Level * 2), 0);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} sneak attacks {target} for {damage} damage!");
    }

}
