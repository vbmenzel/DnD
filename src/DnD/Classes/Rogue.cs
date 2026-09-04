using System;

/// <summary>
/// Summary description for Rogue
/// </summary>
public class Rogue : Character
{
    public Rogue(string name, int level, int maxHealth, int baseAttack, int baseDefense) : base(name, level, maxHealth, baseAttack, baseDefense)
    {

    }
    public override void Attack(IDamageable target)
    {
        int damage = BaseAttack; //+ BonusDamage skal implementeres // Example damage calculation
        if (damage < 0) damage = 0; // Ensure damage is not negative
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!"); // Overvej at udvide med kritiske hits eller andre effekter, der er typiske for en Rogue-klasse.
    }

}
