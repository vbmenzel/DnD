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
            Monster? target = GetFirstLivingMonster();

            if (target is null)
            {
                return;
            }

            ResolveAttack(character, target);
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

            ResolveAttack(monster, target);
        }
    }

    /// <summary>
    /// Resolves an attack roll against the target's defense and applies the
    /// attack when it hits.
    /// </summary>
    /// <param name="attacker">The character performing the attack.</param>
    /// <param name="target">The character being attacked.</param>
    private void ResolveAttack(Character attacker, Character target)
    {
        int roll = _diceRoller.Roll(_attackDieSides);
        long attackScore = (long)roll + attacker.Level;
        long defenseScore = (long)target.BaseDefense + target.Level;

        // The die's maximum result is an automatic hit. For all other results,
        // one misses and the remaining rolls compare attack and defense scores.
        bool attackHits = roll == _attackDieSides ||
            (roll != 1 && attackScore >= defenseScore);

        if (attackHits)
        {
            attacker.Attack(target);

            if (target.IsDefeated)
            {
                Console.WriteLine($"{target} has been defeated!");
            }

            return;
        }

        Console.WriteLine($"{attacker.Name} missed {target.Name}.");
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
