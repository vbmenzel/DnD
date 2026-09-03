using System;
using Interfaces;
public abstract class Character: IDamageable
{
	// Either change the UML diagram to fit "HP" or change the fields to match the UML diagram - Side note IDamageable also requires "CurrentHealth"
	public int HP { get; private set; }
	public int MaxHP { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public int Level { get; private set; }
	// Add these new fields to the UML diagram
    public int BaseDefense { get; private set; }
    public int Xp { get; private set; }
	public int BaseAttack { get; private set; }

	// CurrentHealth doesn't exist, but according to the UML diagram it should
    public bool IsDefeated => CurrentHealth <= 0;

	// I think you accidentally brought back the abstract constructor
	// Might also be worth considering making it a protected constructor instead since outside code shouldn't be able to construct it anyway
    public abstract Character(string name, int level, int maxHP, int baseAttack, int baseDefense)
    {
        Name = name;
        Level = level;
        MaxHP = maxHP;
        HP = MaxHP;
        Xp = 0;
        BaseAttack = baseAttack;
        BaseDefense = baseDefense;
    }

	// UML diagram calls for "public abstract void Attack(Character target);" 
	// but IDamageable as the target also makes sense, so we should either change the type or change the UML diagram
    public abstract void Attack(IDamageable target);


    public void TakeDamage(int amount)
    {
        HP -= amount;

        if (HP < 0)
        {
            HP = 0;
        }
    }

    public void Heal(int amount)
    {
        HP += amount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }
}
