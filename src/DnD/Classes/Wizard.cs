using System;
namespace DnD;

/// <summary>
/// Summary description for Wizard
/// </summary>
public class Wizard : Character, ISpellcaster
{
	public int MaxMana { get; private set; }
	public int CurrentMana { get; private set; }
	public Wizard(string name, int level, int maxHealth, int baseAttack, int baseDefense) : base(name, level, maxHealth , baseAttack, baseDefense)
	{
		MaxMana = 100; // Example maximum mana value
	}

    public override void Attack(IDamageable target)
    {
        int damage = BaseAttack; //+ BonusDamage skal implementeres // Example damage calculation
        if (damage < 0) damage = 0; // Ensure damage is not negative
        target.TakeDamage(damage);
        Console.WriteLine($"{Name} attacks {target} for {damage} damage!"); // Overvej at udvide med kritiske hits eller andre effekter, der er typiske for en Wizard-klasse.
    }

    public void CastSpell(IDamageable target)
    {
        if (CurrentMana >= 20) // Example mana cost for a spell
        {
            CurrentMana -= 20; // Deduct mana
            int damage = 50; // Example spell damage
            target.TakeDamage(damage);
            Console.WriteLine($"{Name} casts a spell on {target} for {damage} damage!"); // Overvej at udvide med kritiske hits eller andre effekter, der er typiske for en Wizard-klasse.
        }
        else
        {
            Console.WriteLine($"{Name} does not have enough mana to cast a spell!");
        }
    }
}
