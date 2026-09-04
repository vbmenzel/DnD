namespace DnD.Combat.Actions;

/// <summary>
/// Specifies which kind of character may be selected for a combat action.
/// </summary>
public enum CombatTargetType
{
    /// <summary>
    /// The action targets a living opponent.
    /// </summary>
    Enemy,

    /// <summary>
    /// The action targets a living member of the actor's party.
    /// </summary>
    Ally,

    /// <summary>
    /// The action targets the character performing it.
    /// </summary>
    Self,
}
