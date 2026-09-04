using System;
using Interfaces;
public abstract class Character: IDamageable
{
	public int CurrentHealth { get; private set; }
	public int MaxHealth { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public int Level { get; private set; }
	// Add these new fields to the UML diagram
    public int BaseDefense { get; private set; }
    public int Xp { get; private set; }
	public int BaseAttack { get; private set; }

    public bool IsDefeated => CurrentHealth <= 0;


    public protected Character(string name, int level, int maxHealth, int baseAttack, int baseDefense)
    {
        Name = name;
        Level = level;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        Xp = 0;
        BaseAttack = baseAttack;
        BaseDefense = baseDefense;
    }

	// UML diagram calls for "public abstract void Attack(Character target);" 
	// but IDamageable as the target also makes sense, so we should either change the type or change the UML diagram
    public abstract void Attack(IDamageable target);


    public override string ToString()
    {
        return $"{Name}";
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth < 0)
        {
            Console.WriteLine($"{Name} has been defeated!");
            CurrentHealth = 0;
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
