using DnD.Characters;
using DnD.Combat.Actions;
using DnD.Interfaces;

namespace DnD.Combat;

/// <summary>
/// Resolves attack rolls and executes combat actions.
/// </summary>
internal sealed class CombatActionResolver
{
    private readonly IDiceRoller _diceRoller;
    private readonly int _attackDieSides;

    /// <summary>
    /// Initializes a new instance of the <see cref="CombatActionResolver"/>
    /// class.
    /// </summary>
    /// <param name="diceRoller">The dice roller used for attack rolls.</param>
    /// <param name="attackDieSides">The number of sides on the attack die.</param>
    public CombatActionResolver(IDiceRoller diceRoller, int attackDieSides)
    {
        ArgumentNullException.ThrowIfNull(diceRoller);

        _diceRoller = diceRoller;
        _attackDieSides = attackDieSides;
    }

    /// <summary>
    /// Resolves and executes an action against a selected target.
    /// </summary>
    /// <param name="attacker">The character performing the action.</param>
    /// <param name="action">The combat action being performed.</param>
    /// <param name="target">The character affected by the action.</param>
    public void Resolve(
        Character attacker,
        CombatAction action,
        Character target)
    {
        if (!action.RequiresAttackRoll)
        {
            action.Execute(target);
            DisplayDefeatIfNeeded(target);
            return;
        }

        int roll = _diceRoller.Roll(_attackDieSides);
        long attackScore = (long)roll + attacker.Level + action.AttackRollModifier;
        long defenseScore = (long)target.BaseDefense + target.Level +
            target.DefenseBonus;

        // The die's maximum result is an automatic hit. For all other results,
        // one misses and the remaining rolls compare attack and defense scores.
        bool attackHits = roll == _attackDieSides ||
            (roll != 1 && attackScore >= defenseScore);

        if (attackHits)
        {
            action.Execute(target);
            DisplayDefeatIfNeeded(target);
            return;
        }

        Console.WriteLine(
            $"{attacker.Name} missed {target.Name} with {action.Name}.");
    }

    /// <summary>
    /// Displays a message when an action defeats its target.
    /// </summary>
    /// <param name="target">The target affected by the action.</param>
    private static void DisplayDefeatIfNeeded(Character target)
    {
        if (target.IsDefeated)
        {
            Console.WriteLine($"{target} has been defeated!");
        }
    }
}
