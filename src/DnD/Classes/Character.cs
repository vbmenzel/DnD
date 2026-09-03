using System;
using Interfaces;
public abstract class Character: IDamageable
{
	int HP { get; private set; }
	int MaxHP { get; private set; }
	string Name { get; private set; } = string.Empty;
	int Level { get; private set; }
	int Xp { get; private set; }
	int AttackPower { get; private set; }
    


	public abstract Character(string name, int level, int maxHP, int attackPower)
    {
        Name = name;
        Level = level;
        MaxHP = maxHP;
        HP = MaxHP;
        Xp = 0;
        AttackPower = attackPower;
    }

    public abstract void Attack(IDamageable target);

    public abstract void TakeDamage(int amount);

    public abstract void Heal(int amount);



}
