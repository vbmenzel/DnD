using System;
using Interfaces;
public abstract class Character: IDamageable
{
	public int HP { get; private set; }
	public int MaxHP { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public int Level { get; private set; }
    public int BaseDefense { get; private set; }
    public int Xp { get; private set; }
	public int BaseAttack { get; private set; }

    public bool IsDefeated => CurrentHealth <= 0;

    public Character(string name, int level, int maxHP, int baseAttack, int baseDefense)
    {
        Name = name;
        Level = level;
        MaxHP = maxHP;
        HP = MaxHP;
        Xp = 0;
        BaseAttack = baseAttack;
        BaseDefense = baseDefense;
    }

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
{



}
