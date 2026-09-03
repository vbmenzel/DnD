namespace DnD.Interfaces;

/// <summary>Defines the contract for entities that can take damage.</summary>
public interface IDamageable
{
    /// <summary>Gets the current health points of the entity.</summary>
    int CurrentHealth { get; }

    /// <summary>Gets a value indicating whether the entity has been defeated.</summary>
    bool IsDefeated { get; }

    /// <summary>Applies damage to the entity, reducing its current health.</summary>
    /// <param name="amount">The amount of damage to apply.</param>
    void TakeDamage(int amount);
}
