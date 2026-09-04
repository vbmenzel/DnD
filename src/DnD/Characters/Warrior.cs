using System;
using DnD.Combat.Actions;
using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Summary description for Warrior
/// </summary>
public class Warrior: Character
{

	public Warrior(string name, int level, int maxHP, int baseAttack, int baseDefense) : base(name, level, maxHP, baseAttack, baseDefense)
	{

	}

    public override void Attack(IDamageable target)
    {
        int damage = BaseAttack + DamageBonus;
        if (damage < 0) damage = 0; // Ensure damage is not negative
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!"); //overvej at skrive overkill eller noget hvis mm
    }

    /// <inheritdoc />
    protected override IReadOnlyList<CombatAction> GetClassCombatActions()
    {
        return
        [
            new CombatAction(
                "Attack",
                CombatTargetType.Enemy,
                true,
                target => Attack(target)),
            new CombatAction(
                "Heavy attack",
                CombatTargetType.Enemy,
                true,
                HeavyAttack,
                -3),
        ];
    }

    /// <summary>
    /// Performs a stronger attack with a reduced chance to hit.
    /// </summary>
    /// <param name="target">The character receiving the attack.</param>
    private void HeavyAttack(Character target)
    {
        int damage = Math.Max(BaseAttack + DamageBonus + Level, 0);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} uses a heavy attack on {target} for {damage} damage!");
    }

}
