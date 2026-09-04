using DnD.Combat.Actions;
using DnD.Interfaces;
using DnD.Items;

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
    public Inventory Inventory { get; } = new Inventory();

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
    /// Gets the class and inventory actions currently available to this
    /// character.
    /// </summary>
    /// <returns>The available combat actions.</returns>
    public IReadOnlyList<CombatAction> GetCombatActions()
    {
        List<CombatAction> actions = GetClassCombatActions().ToList();

        foreach (Potion potion in Inventory.GetItems().OfType<Potion>())
        {
            actions.Add(new CombatAction(
                $"Use {potion.Name}",
                CombatTargetType.Ally,
                false,
                target => UsePotion(potion, target),
                canTarget: target => !target.IsDefeated && target.HP < target.MaxHP));
        }

        return actions;
    }

    /// <summary>
    /// Gets the combat actions supplied by this character's class.
    /// </summary>
    /// <returns>The class-specific combat actions.</returns>
    protected abstract IReadOnlyList<CombatAction> GetClassCombatActions();

    /// <summary>
    /// Uses and removes a potion from this character's inventory.
    /// </summary>
    /// <param name="potion">The potion being used.</param>
    /// <param name="target">The character receiving the healing.</param>
    private void UsePotion(Potion potion, Character target)
    {
        int healthBeforeHealing = target.HP;

        potion.Use(target);
        Inventory.RemoveItem(potion);

        int restoredHealth = target.HP - healthBeforeHealing;
        Console.WriteLine(
            $"{Name} uses {potion.Name} on {target}, restoring {restoredHealth} health!");
    }

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
