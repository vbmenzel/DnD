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

    // Tracking the encounter number here lets a later adventure continue at
    // the same difficulty instead of restarting from the first encounter.
    private int _encounterNumber;

    /// <summary>
    /// Initializes a new instance of the <see cref="Adventure"/> class.
    /// </summary>
    /// <param name="party">The party undertaking the adventure.</param>
    /// <param name="diceRoller">The dice roller used during encounters.</param>
    /// <param name="startingEncounterNumber">
    /// The int-based number of the first encounter to fight. The default is 1.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="startingEncounterNumber"/> is less than one.
    /// </exception>
    public Adventure(
        Party party,
        IDiceRoller diceRoller,
        int startingEncounterNumber = 1)
    {
        ArgumentNullException.ThrowIfNull(party);
        ArgumentNullException.ThrowIfNull(diceRoller);

        if (startingEncounterNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(startingEncounterNumber));
        }

        _party = party;
        _diceRoller = diceRoller;
        _encounterNumber = startingEncounterNumber;
    }

    /// <summary>
    /// Gets the number of the next encounter the party must face.
    /// </summary>
    public int NextEncounterNumber => _encounterNumber;

    /// <summary>
    /// Runs the encounters and travel scenes that make up the adventure.
    /// </summary>
    public void Start()
    {

        Game.GameLogger.StartNewLog();
        Game.GameLogger.Log("The adventure begins!");

        // The adventure continues until combat defeats every party member or,
        // after every third encounter, the party returns to the tavern.
        while (true)
        {
            // Begin with combat; travel is shown only between encounters.
            if (_encounterNumber > 1)
            {
                TravelNarrator.Narrate(_diceRoller);
            }

            IReadOnlyList<Monster> monsters = MonsterGenerator.Generate(
                _encounterNumber,
                _diceRoller);
            var encounter = new Encounter(_party, monsters, _diceRoller);

            Game.GameLogger.Log("");
            Game.GameLogger.Log($"Encounter {_encounterNumber} begins!");
            EncounterResult result = encounter.Start();

            AwardExperience(result);
            RestorePartyMana();
            AwardLoot(result);

            if (!result.PartyWon)
            {
                Game.GameLogger.Log("The adventure has come to an end.");
                return;
            }

            int completedEncounter = _encounterNumber;
            _encounterNumber++;

            if (completedEncounter % 3 == 0 && ShouldReturnToTavern())
            {
                Game.GameLogger.Log("The party returns to the tavern.");
                return;
            }
        }
    }

    /// <summary>
    /// Asks the player whether the party should return to the tavern.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> when the party returns to the tavern; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    private static bool ShouldReturnToTavern()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("The party can rest at the tavern...");
        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[1] >> Continue Adventuring");
        Console.WriteLine("[2] >> Return to Tavern");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Choose your path > ");
        Console.ResetColor();

        string? choice = Console.ReadLine();
        Console.WriteLine();

        return choice != null && choice.Trim() == "2";
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

        Game.GameLogger.Log(
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
    /// Restores up to half of each living spellcaster's maximum mana between
    /// encounters.
    /// </summary>
    private void RestorePartyMana()
    {
        foreach (Character character in GetLivingPartyMembers())
        {
            if (character is not ISpellcaster spellcaster)
            {
                continue;
            }

            int manaBeforeRestoration = spellcaster.CurrentMana;
            int restorationAmount = spellcaster.MaxMana / 2;

            spellcaster.RestoreMana(restorationAmount);

            int restoredMana = spellcaster.CurrentMana - manaBeforeRestoration;

            if (restoredMana == 0)
            {
                continue;
            }

            Game.GameLogger.Log(
                $"{character.Name} recovers {restoredMana} mana.");
        }
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
        Game.GameLogger.Log($"{recipient.Name} receives {item.Name}.");
    }
}
