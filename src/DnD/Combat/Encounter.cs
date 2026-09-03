using DnD.Characters;
using DnD.Combat.Dice;
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
    private readonly Party _party;
    private readonly List<Monster> _monsters;
    private readonly IDiceRoller _diceRoller;

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
        IDiceRoller diceRoller)
    {
        ArgumentNullException.ThrowIfNull(party);
        ArgumentNullException.ThrowIfNull(monsters);
        ArgumentNullException.ThrowIfNull(diceRoller);

        _party = party;
        _monsters = monsters.ToList();
        _diceRoller = diceRoller;

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
            Monster? target = GetFirstLivingMonster();

            if (target is null)
            {
                return;
            }

            character.Attack(target);
        }
    }

    /// <summary>
    /// Executes a turn for each living monster in the encounter.
    /// </summary>
    public void MonsterTurn()
    {
        foreach (Monster monster in _monsters.Where(monster => !monster.IsDefeated))
        {
            Character? target = GetFirstLivingPartyMember();

            if (target is null)
            {
                return;
            }

            monster.Attack(target);
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
    /// Finds the first living monster in the encounter.
    /// </summary>
    /// <returns>
    /// The first living monster, or <see langword="null"/> when none remain.
    /// </returns>
    private Monster? GetFirstLivingMonster()
    {
        return _monsters.FirstOrDefault(monster => !monster.IsDefeated);
    }

    /// <summary>
    /// Finds the first living member of the party.
    /// </summary>
    /// <returns>
    /// The first living party member, or <see langword="null"/> when none
    /// remain.
    /// </returns>
    private Character? GetFirstLivingPartyMember()
    {
        return _party.GetMembers().FirstOrDefault(character => !character.IsDefeated);
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
