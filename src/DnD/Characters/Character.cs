using DnD.Interfaces;
using DnD.Combat.Actions;

namespace DnD.Characters;

public abstract class Character : IDamageable
{
    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Level { get; private set; }
    public int BaseDefense { get; private set; }
    public int Xp { get; private set; }
    public int BaseAttack { get; private set; }

    public int CurrentHealth => HP;

	// CurrentHealth doesn't exist
    public bool IsDefeated => CurrentHealth <= 0;

    protected Character(string name, int level, int maxHP, int baseAttack, int baseDefense)
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

    /// <summary>
    /// Gets the combat actions currently available to this character.
    /// </summary>
    /// <returns>The available combat actions.</returns>
    public abstract IReadOnlyList<CombatAction> GetCombatActions();

    public override string ToString()
    {
        return $"{Name}";
    }

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
