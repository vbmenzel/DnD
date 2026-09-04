using DnD.Characters;
using DnD.Combat.Actions;
using DnD.Combat.Exceptions;
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
    private readonly CombatActionResolver _actionResolver;

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
        _actionResolver = new CombatActionResolver(diceRoller, attackDieSides);

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
    /// <returns>The result of the completed encounter.</returns>
    public EncounterResult Start()
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

        bool partyWon = AreMonstersDefeated();
        DisplayResult(partyWon);

        return new EncounterResult(
            partyWon,
            _monsters.Where(monster => monster.IsDefeated));
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

            CombatAction action = CombatConsole.SelectAction(character, actions);
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
    /// Resolves an action and displays recoverable combat errors without
    /// terminating the encounter.
    /// </summary>
    /// <param name="attacker">The character performing the action.</param>
    /// <param name="action">The combat action being performed.</param>
    /// <param name="target">The character affected by the action.</param>
    private void ResolveAction(
        Character attacker,
        CombatAction action,
        Character target)
    {
        try
        {
            _actionResolver.Resolve(attacker, action, target);
        }
        catch (CharacterIsDefeatedException exception)
        {
            Console.WriteLine(exception.Message);
        }
        catch (InsufficientManaException exception)
        {
            Console.WriteLine(exception.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
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

        return CombatConsole.SelectTarget(targets);
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
    /// <param name="partyWon">
    /// A value indicating whether the party defeated every monster.
    /// </param>
    private static void DisplayResult(bool partyWon)
    {
        string result = partyWon
            ? "The party won the encounter!"
            : "The monsters won the encounter!";

        Console.WriteLine(result);
    }
}
