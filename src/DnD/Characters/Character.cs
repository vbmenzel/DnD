using DnD.Combat.Actions;
using DnD.Interfaces;
using DnD.Items;

namespace DnD.Characters;

public abstract class Character : IDamageable
{
    private const int BaseExperienceRequirement = 100;
    private const int ExperienceRequirementIncrease = 50;

    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Level { get; private set; }
    public int BaseDefense { get; private set; }
    public int Xp { get; private set; }
    public int BaseAttack { get; private set; }
    public Inventory Inventory { get; } = new Inventory();

    public int CurrentHealth => HP;
    public bool IsDefeated => HP <= 0;

    /// <summary>
    /// Gets the damage bonus from the equipped weapon.
    /// </summary>
    public int DamageBonus =>
        Inventory.GetEquippedItem(EquipmentSlot.Weapon) is Weapon weapon
            ? weapon.DamageBonus
            : 0;

    /// <summary>
    /// Gets the defense bonus from the equipped armor.
    /// </summary>
    public int DefenseBonus =>
        Inventory.GetEquippedItem(EquipmentSlot.Armor) is Armor armor
            ? armor.DefenseBonus
            : 0;

    /// <summary>
    /// Gets the experience required to advance from the current level.
    /// </summary>
    public int ExperienceRequiredForNextLevel =>
        BaseExperienceRequirement + ((Level - 1) * ExperienceRequirementIncrease);

    /// <summary>
    /// Initializes the state shared by every character.
    /// </summary>
    /// <param name="name">The character's name.</param>
    /// <param name="level">The character's initial level.</param>
    /// <param name="maxHP">The character's maximum health points.</param>
    /// <param name="baseAttack">The character's base attack value.</param>
    /// <param name="baseDefense">The character's base defense value.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="maxHP"/> is less than one.
    /// </exception>
    protected Character(string name, int level, int maxHP, int baseAttack, int baseDefense)
    {
        if (maxHP < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxHP),
                "Maximum health must be at least one.");
        }

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

    /// <summary>
    /// Reduces the character's health without allowing it to fall below zero.
    /// </summary>
    /// <param name="amount">The amount of damage to apply.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="amount"/> is negative.
    /// </exception>
    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Damage cannot be negative.");
        }

        HP = Math.Max(HP - amount, 0);
    }

    /// <summary>
    /// Restores the character's health without exceeding maximum health.
    /// </summary>
    /// <param name="amount">The amount of health to restore.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="amount"/> is negative.
    /// </exception>
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Healing cannot be negative.");
        }

        HP = (int)Math.Min((long)HP + amount, MaxHP);
    }

    /// <summary>
    /// Adds experience and applies every level gained from it.
    /// </summary>
    /// <param name="amount">The amount of experience to add.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="amount"/> is negative.
    /// </exception>
    public void GainExperience(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Experience cannot be negative.");
        }

        Xp += amount;

        while (Xp >= ExperienceRequiredForNextLevel)
        {
            Xp -= ExperienceRequiredForNextLevel;
            Level++;

            Console.WriteLine($"{Name} reached level {Level}!");
        }
    }
}
