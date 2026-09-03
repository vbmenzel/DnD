using DnD.Characters;
using DnD.Combat.Dice;
using DnD.Interfaces;

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
    /// Starts the encounter and continues the combat until either the party or all monsters have been defeated.
    /// </summary>
    public void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Executes a turn for the living members of the party.
    /// </summary>
    public void PlayerTurn()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Executes a turn for each living monster in the encounter.
    /// </summary>
    public void MonsterTurn()
    {
        throw new NotImplementedException();
    }
}
