using DnD.Characters;
using DnD.Combat.Actions;
using DnD.Interfaces;
using DnD.Parties;

namespace DnD.Combat;

/// <summary>
/// Represents a turn-based combat encounter between a party
/// and a group of monsters.
/// </summary>
/// <remarks>
/// The encounter coordinates player and monster turns and uses an
/// <see cref="IDiceRoller"/> for any dice rolls required during combat.
/// </remarks>
public class Encounter
{
    private const int DefaultAttackDieSides = 20;

    private readonly Party _party;
    private readonly List<Monster> _monsters;
    private readonly IDiceRoller _diceRoller;
    private readonly int _attackDieSides;

    /// <summary>
    /// Initializes a new instance of the <see cref="Encounter"/> class.
    /// </summary>
    /// <param name="party">
    /// The party participating in the encounter.
    /// </param>
    /// <param name="monsters">
    /// The monsters opposing the party.
    /// </param>
    /// <param name="diceRoller">
    /// The dice roller used to determine random combat outcomes.
    /// </param>
    /// <param name="attackDieSides">
    /// The number of sides on the die used for attack rolls. The default is 20.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="party"/>,
    /// <paramref name="monsters"/>, or
    /// <paramref name="diceRoller"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="monsters"/> does not contain
    /// at least one monster.
    /// </exception>
    public Encounter(
        Party party,
        IEnumerable<Monster> monsters,
        IDiceRoller diceRoller,
        int attackDieSides = DefaultAttackDieSides)
    {
        ArgumentNullException.ThrowIfNull(party);
        ArgumentNullException.ThrowIfNull(monsters);
        ArgumentNullException.ThrowIfNull(diceRoller);

        _party = party;
        _monsters = monsters.ToList();
        _diceRoller = diceRoller;

        _attackDieSides = attackDieSides;

        if (_monsters.Count == 0)
        {
            throw new ArgumentException(
                "An encounter must have at least one monster.",
                nameof(monsters));
        }
    }

    /// <summary>
    /// Starts the encounter and continues combat until either the party or all
    /// monsters have been defeated.
    /// </summary>
    public void Start()
    {
        while (!IsPartyDefeated() && !AreMonstersDefeated())
        {
            PlayerTurn();

            if (AreMonstersDefeated())
            {
                break;
            }

            MonsterTurn();
        }

        DisplayResult();
    }

    /// <summary>
    /// Executes a turn for the living members of the party.
    /// </summary>
    public void PlayerTurn()
    {
        foreach (Character character in _party.GetMembers().Where(
                     character => !character.IsDefeated))
        {
            IReadOnlyList<CombatAction> actions = GetUsableActions(character);

            if (actions.Count == 0)
            {
                return;
            }

            CombatAction action = SelectCombatAction(character, actions);
            Character target = SelectTarget(character, action);

            ResolveAction(character, action, target);
        }
    }

    /// <summary>
    /// Executes a turn for each living monster in the encounter.
    /// </summary>
    public void MonsterTurn()
    {
        foreach (Monster monster in _monsters.Where(monster => !monster.IsDefeated))
        {
            CombatAction? action = GetUsableActions(monster).FirstOrDefault();

            if (action is null)
            {
                return;
            }

            Character target = GetValidTargets(monster, action)[0];
            ResolveAction(monster, action, target);
        }
    }

    /// <summary>
    /// Resolves and executes a combat action against a selected target.
    /// </summary>
    /// <param name="attacker">The character performing the action.</param>
    /// <param name="action">The combat action being performed.</param>
    /// <param name="target">The character being attacked.</param>
    private void ResolveAction(
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
        long defenseScore = (long)target.BaseDefense + target.Level;

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
    /// Gets the actions that currently have at least one valid target.
    /// </summary>
    /// <param name="character">The character whose actions are requested.</param>
    /// <returns>The actions that the character can currently perform.</returns>
    private IReadOnlyList<CombatAction> GetUsableActions(Character character)
    {
        return character.GetCombatActions()
            .Where(action => GetValidTargets(character, action).Count > 0)
            .ToList();
    }

    /// <summary>
    /// Prompts the player to select one of a character's available actions.
    /// </summary>
    /// <param name="character">The character taking a turn.</param>
    /// <param name="actions">The actions available to the character.</param>
    /// <returns>The selected combat action.</returns>
    private static CombatAction SelectCombatAction(
        Character character,
        IReadOnlyList<CombatAction> actions)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"{character.Name}'s turn ({character.HP}/{character.MaxHP} HP):");

        for (int index = 0; index < actions.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {actions[index].Name}");
        }

        int selectedIndex = ReadSelection(actions.Count);
        return actions[selectedIndex];
    }

    /// <summary>
    /// Prompts the player to select a valid target for an action.
    /// </summary>
    /// <param name="actor">The character performing the action.</param>
    /// <param name="action">The selected combat action.</param>
    /// <returns>The selected target.</returns>
    private Character SelectTarget(Character actor, CombatAction action)
    {
        IReadOnlyList<Character> targets = GetValidTargets(actor, action);

        if (action.TargetType == CombatTargetType.Self)
        {
            return actor;
        }

        Console.WriteLine("Choose a target:");

        for (int index = 0; index < targets.Count; index++)
        {
            Character target = targets[index];
            Console.WriteLine(
                $"{index + 1}. {target.Name} ({target.HP}/{target.MaxHP} HP)");
        }

        int selectedIndex = ReadSelection(targets.Count);
        return targets[selectedIndex];
    }

    /// <summary>
    /// Gets the valid targets for an action performed by a character.
    /// </summary>
    /// <param name="actor">The character performing the action.</param>
    /// <param name="action">The action whose targets are requested.</param>
    /// <returns>The living characters accepted by the action.</returns>
    private IReadOnlyList<Character> GetValidTargets(
        Character actor,
        CombatAction action)
    {
        bool actorIsMonster = actor is Monster monster && _monsters.Contains(monster);

        IEnumerable<Character> targets = action.TargetType switch
        {
            CombatTargetType.Enemy => actorIsMonster
                ? _party.GetMembers()
                : _monsters,
            CombatTargetType.Ally => actorIsMonster
                ? _monsters
                : _party.GetMembers(),
            CombatTargetType.Self => [actor],
            _ => throw new InvalidOperationException(
                "The combat action has an unsupported target type."),
        };

        return targets
            .Where(target => !target.IsDefeated && action.CanTarget(target))
            .ToList();
    }

    /// <summary>
    /// Reads a one-based menu selection from the console.
    /// </summary>
    /// <param name="optionCount">The number of available menu options.</param>
    /// <returns>The selected option's zero-based index.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the standard input stream is closed.
    /// </exception>
    private static int ReadSelection(int optionCount)
    {
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine()
                ?? throw new InvalidOperationException(
                    "Cannot select a combat action because input is unavailable.");

            if (int.TryParse(input, out int selection) &&
                selection >= 1 &&
                selection <= optionCount)
            {
                return selection - 1;
            }

            Console.WriteLine($"Enter a number between 1 and {optionCount}.");
        }
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

    /// <summary>
    /// Determines whether every member of the party has been defeated.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> when no living party members remain; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    private bool IsPartyDefeated()
    {
        return _party.GetMembers().All(character => character.IsDefeated);
    }

    /// <summary>
    /// Determines whether every monster in the encounter has been defeated.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> when no living monsters remain; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    private bool AreMonstersDefeated()
    {
        return _monsters.All(monster => monster.IsDefeated);
    }

    /// <summary>
    /// Displays the result of the encounter.
    /// </summary>
    private void DisplayResult()
    {
        string result = AreMonstersDefeated()
            ? "The party won the encounter!"
            : "The monsters won the encounter!";

        Console.WriteLine(result);
    }
}
