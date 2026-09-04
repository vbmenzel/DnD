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
    public override void Attack(IDamageable target)
    {
        int damage = BaseAttack; //+ BonusDamage skal implementeres // Example damage calculation
        if (damage < 0) damage = 0; // Ensure damage is not negative
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!"); // Overvej at udvide med kritiske hits eller andre effekter, der er typiske for en Rogue-klasse.
    }

    /// <inheritdoc />
    public override IReadOnlyList<CombatAction> GetCombatActions()
    {
        return
        [
            new CombatAction(
                "Attack",
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

    private void SneakAttack(Character target)
    {
        int damage = Math.Max(BaseAttack + Level, 0);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} sneak attacks {target} for {damage} damage!");
    }

}
