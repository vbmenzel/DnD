using System;
using DnD.Combat.Actions;
using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Summary description for Wizard
/// </summary>
public class Wizard : Character
{
	public Wizard(string name, int level, int maxHP, int baseAttack, int baseDefense) : base(name, level, maxHP, baseAttack, baseDefense)
	{

	}

    public override void Attack(IDamageable target)
    {
        int damage = BaseAttack; //+ BonusDamage skal implementeres // Example damage calculation
        if (damage < 0) damage = 0; // Ensure damage is not negative
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!"); // Overvej at udvide med kritiske hits eller andre effekter, der er typiske for en Wizard-klasse.
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
        ];
    }
}
