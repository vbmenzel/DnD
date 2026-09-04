using System;
namespace DnD;

/// <summary>
/// Summary description for Warrior
/// </summary>
public class Warrior: Character
{

	public Warrior(string name, int level, int maxHealth, int baseAttack, int baseDefense) : base(name, level, maxHealth, baseAttack, baseDefense)
	{

	}

    public override void Attack(IDamageable target)
    {
        int damage = BaseAttack; //+ BonusDamage skal implementeres // Example damage calculation
        if (damage < 0) damage = 0; // Ensure damage is not negative
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!"); //overvej at skrive overkill eller noget hvis mm
    }

}
