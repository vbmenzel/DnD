using DnD.Characters;

namespace DnD.Combat.Actions;

/// <summary>
/// Describes an action that a character can perform during combat.
/// </summary>
public sealed class CombatAction
{
    private readonly Action<Character> _execute;

    /// <summary>
    /// Initializes a new instance of the <see cref="CombatAction"/> class.
    /// </summary>
    /// <param name="name">The name displayed when the action is selected.</param>
    /// <param name="targetType">The kind of target accepted by the action.</param>
    /// <param name="requiresAttackRoll">
    /// A value indicating whether the action must pass an attack roll.
    /// </param>
    /// <param name="execute">The behavior performed against the selected target.</param>
    /// <param name="attackRollModifier">
    /// The value added to or subtracted from the actor's attack roll.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is empty or consists only of white-space
    /// characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="name"/> or <paramref name="execute"/> is
    /// <see langword="null"/>.
    /// </exception>
    public CombatAction(
        string name,
        CombatTargetType targetType,
        bool requiresAttackRoll,
        Action<Character> execute,
        int attackRollModifier = 0)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(execute);

        Name = name;
        TargetType = targetType;
        RequiresAttackRoll = requiresAttackRoll;
        AttackRollModifier = attackRollModifier;
        _execute = execute;
    }

    /// <summary>
    /// Gets the name displayed when the action is selected.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the kind of target accepted by the action.
    /// </summary>
    public CombatTargetType TargetType { get; }

    /// <summary>
    /// Gets a value indicating whether the action must pass an attack roll.
    /// </summary>
    public bool RequiresAttackRoll { get; }

    /// <summary>
    /// Gets the value added to or subtracted from the actor's attack roll.
    /// </summary>
    public int AttackRollModifier { get; }

    /// <summary>
    /// Performs the action against the selected target.
    /// </summary>
    /// <param name="target">The character affected by the action.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    public void Execute(Character target)
    {
        ArgumentNullException.ThrowIfNull(target);
        _execute(target);
    }
}
