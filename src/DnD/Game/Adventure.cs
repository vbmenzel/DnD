using DnD.Characters;
using DnD.Combat;
using DnD.Interfaces;
using DnD.Items;
using DnD.Parties;

namespace DnD.Game;

/// <summary>
/// Coordinates a sequence of encounters connected by short travel scenes.
/// </summary>
internal sealed class Adventure
{
    // Reusing the same Party instance preserves health, XP, and inventory
    // changes between encounters.
    private readonly Party _party;
    private readonly IDiceRoller _diceRoller;

    /// <summary>
    /// Initializes a new instance of the <see cref="Adventure"/> class.
    /// </summary>
    /// <param name="party">The party undertaking the adventure.</param>
    /// <param name="diceRoller">The dice roller used during encounters.</param>
    public Adventure(Party party, IDiceRoller diceRoller)
    {
        ArgumentNullException.ThrowIfNull(party);
        ArgumentNullException.ThrowIfNull(diceRoller);

        _party = party;
        _diceRoller = diceRoller;
    }

    /// <summary>
    /// Runs the encounters and travel scenes that make up the adventure.
    /// </summary>
    public void Start()
    {
        Console.WriteLine("The adventure begins!");

        int encounterNumber = 1;

        // There is deliberately no final encounter. The adventure continues
        // until combat defeats every party member.
        while (true)
        {
            // Begin with combat; travel is shown only between encounters.
            if (encounterNumber > 1)
            {
                TravelNarrator.Narrate(_diceRoller);
            }

            IReadOnlyList<Monster> monsters = MonsterGenerator.Generate(
                encounterNumber,
                _diceRoller);
            var encounter = new Encounter(_party, monsters, _diceRoller);

            Console.WriteLine();
            Console.WriteLine($"Encounter {encounterNumber} begins!");
            EncounterResult result = encounter.Start();

            AwardExperience(result);
            AwardLoot(result);

            if (!result.PartyWon)
            {
                Console.WriteLine("The adventure has come to an end.");
                return;
            }

            encounterNumber++;
        }
    }

    /// <summary>
    /// Awards the defeated monsters' combined experience to every living
    /// party member.
    /// </summary>
    /// <param name="result">The completed encounter's result.</param>
    private void AwardExperience(EncounterResult result)
    {
        int experience = CalculateExperienceReward(result);

        if (experience == 0)
        {
            return;
        }

        IReadOnlyList<Character> livingMembers = GetLivingPartyMembers();

        if (livingMembers.Count == 0)
        {
            return;
        }

        Console.WriteLine(
            $"The surviving party members gain {experience} XP each.");

        // Giving every survivor the full reward keeps individual progression
        // independent of the number of characters in the party.
        foreach (Character character in livingMembers)
        {
            character.GainExperience(experience);
        }
    }

    /// <summary>
    /// Calculates the combined experience reward from defeated monsters.
    /// </summary>
    /// <param name="result">The completed encounter's result.</param>
    /// <returns>The total experience reward for the encounter.</returns>
    private static int CalculateExperienceReward(EncounterResult result)
    {
        return result.DefeatedMonsters.Sum(
            monster => monster.ExperienceReward);
    }

    /// <summary>
    /// Gets the party members eligible to receive encounter experience.
    /// </summary>
    /// <returns>The living members of the party.</returns>
    private IReadOnlyList<Character> GetLivingPartyMembers()
    {
        return _party.GetMembers()
            .Where(character => !character.IsDefeated)
            .ToList();
    }

    /// <summary>
    /// Generates and awards an item to a random living party member.
    /// </summary>
    /// <param name="result">The completed encounter's result.</param>
    private void AwardLoot(EncounterResult result)
    {
        IReadOnlyList<Character> livingMembers = GetLivingPartyMembers();

        if (livingMembers.Count == 0)
        {
            return;
        }

        Item? item = LootGenerator.Generate(
            result.DefeatedMonsters,
            _diceRoller);

        if (item is null)
        {
            return;
        }

        Character recipient = livingMembers[
            _diceRoller.Roll(livingMembers.Count) - 1];

        recipient.Inventory.AddItem(item);
        Console.WriteLine($"{recipient.Name} receives {item.Name}.");
    }
}
