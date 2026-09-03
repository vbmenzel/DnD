using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Represents a character that can participate in combat.
/// </summary>
public abstract class Character : IDamageable
{
    /// <summary>
    /// Gets the character's current health points.
    /// </summary>
    public int CurrentHealth { get; private set; }

    /// <summary>
    /// Gets the character's maximum health points.
    /// </summary>
    public int MaxHealth { get; private set; }

    /// <summary>
    /// Gets the character's name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the character's level.
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    /// Gets the character's base defense value.
    /// </summary>
    public int BaseDefense { get; private set; }

    /// <summary>
    /// Gets the character's accumulated experience points.
    /// </summary>
    public int ExperiencePoints { get; private set; }

    /// <summary>
    /// Gets the character's base attack value.
    /// </summary>
    public int BaseAttack { get; private set; }

    /// <inheritdoc />
    public bool IsDefeated => CurrentHealth <= 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Character"/> class.
    /// </summary>
    /// <param name="name">The character's name.</param>
    /// <param name="level">The character's initial level.</param>
    /// <param name="maxHealth">The character's maximum health points.</param>
    /// <param name="baseAttack">The character's base attack value.</param>
    /// <param name="baseDefense">The character's base defense value.</param>
    protected Character(
        string name,
        int level,
        int maxHealth,
        int baseAttack,
        int baseDefense)
    {
        Name = name;
        Level = level;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        ExperiencePoints = 0;
        BaseAttack = baseAttack;
        BaseDefense = baseDefense;
    }

    /// <summary>
    /// Attacks the specified target.
    /// </summary>
    /// <param name="target">The target that receives the attack.</param>
    public abstract void Attack(IDamageable target);

    /// <inheritdoc />
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }

    /// <summary>
    /// Restores health without exceeding <see cref="MaxHealth"/>.
    /// </summary>
    /// <param name="amount">The amount of health to restore.</param>
    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
