using DnD.Characters;
using DnD.Combat;
using DnD.Interfaces;
using DnD.Parties;

namespace DnD.Game;

/// <summary>
/// Coordinates a sequence of encounters connected by short travel scenes.
/// </summary>
internal sealed class Adventure
{
    private const int MaximumMonsterCount = 3;
    private const int MaximumMonsterLevel = 20;

    private const int MinimumTravelMessages = 1;
    private const int MaximumTravelMessages = 4;
    private const int MinimumTravelDelayMilliseconds = 1_200;
    private const int MaximumTravelDelayMilliseconds = 2_500;

    private static readonly string[] TravelMessages =
    [
        "The party follows a narrow trail through the wilderness.",
        "A cold breeze moves through the trees.",
        "Loose stones crunch beneath the party's boots.",
        "Distant birds fall silent as the party approaches.",
        "The path bends around an old, moss-covered ruin.",
        "Fresh tracks cross the road ahead.",
    ];

    private static readonly string[] MonsterNames =
    [
        "Bandit",
        "Giant rat",
        "Goblin",
        "Skeleton",
        "Wolf",
        "Young orc",
    ];

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
        while (!IsPartyDefeated())
        {
            // Begin with combat; travel is shown only between encounters.
            if (encounterNumber > 1)
            {
                Travel();
            }

            IReadOnlyList<Monster> monsters = CreateMonsters(encounterNumber);
            var encounter = new Encounter(_party, monsters, _diceRoller);

            Console.WriteLine();
            Console.WriteLine($"Encounter {encounterNumber} begins!");
            encounter.Start();

            // TODO: Award XP and dropped items after every encounter.

            if (IsPartyDefeated())
            {
                Console.WriteLine("The adventure has come to an end.");
                return;
            }

            encounterNumber++;
        }
    }

    /// <summary>
    /// Displays a short, randomized journey between encounters.
    /// </summary>
    private static void Travel()
    {
        // Select from a copy to avoid repeating a message during one journey.
        var availableMessages = TravelMessages.ToList();
        int messageCount = Random.Shared.Next(
            MinimumTravelMessages,
            MaximumTravelMessages + 1);

        Console.WriteLine();
        Console.WriteLine("The party continues its journey...");

        for (int messageIndex = 0;
             messageIndex < messageCount;
             messageIndex++)
        {
            Delay();

            int selectedIndex = Random.Shared.Next(availableMessages.Count);
            Console.WriteLine(availableMessages[selectedIndex]);
            availableMessages.RemoveAt(selectedIndex);
        }

        // Pause once more before the next encounter begins.
        Delay();
    }

    /// <summary>
    /// Waits for a short randomized travel delay.
    /// </summary>
    private static void Delay()
    {
        int delayMilliseconds = Random.Shared.Next(
            MinimumTravelDelayMilliseconds,
            MaximumTravelDelayMilliseconds + 1);

        Thread.Sleep(delayMilliseconds);
    }

    /// <summary>
    /// Creates the monsters for an encounter in the adventure.
    /// </summary>
    /// <param name="encounterNumber">The one-based encounter number.</param>
    /// <returns>The monsters participating in the encounter.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="encounterNumber"/> is less than one.
    /// </exception>
    private static IReadOnlyList<Monster> CreateMonsters(int encounterNumber)
    {
        if (encounterNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(encounterNumber));
        }

        // Difficulty increases every two encounters but stops at level 20.
        int difficultyIncrease = Math.Min(
            (encounterNumber - 1) / 2,
            MaximumMonsterLevel - 1);
        int monsterLevel = Math.Min(1 + difficultyIncrease, MaximumMonsterLevel);
        // Maximum group size increases every two encounters, up to three.
        int maximumEncounterSize = Math.Min(
            1 + ((encounterNumber - 1) / 2),
            MaximumMonsterCount);
        int monsterCount = Random.Shared.Next(1, maximumEncounterSize + 1);

        // Select without replacement to avoid duplicate names in one encounter.
        var availableNames = MonsterNames.ToList();
        var monsters = new List<Monster>();

        for (int monsterIndex = 0; monsterIndex < monsterCount; monsterIndex++)
        {
            int nameIndex = Random.Shared.Next(availableNames.Count);
            string name = availableNames[nameIndex];
            availableNames.RemoveAt(nameIndex);

            // Random base values vary monsters within the same difficulty tier.
            int maxHealth = Random.Shared.Next(7, 13) + (difficultyIncrease * 3);
            int baseAttack = Random.Shared.Next(2, 5) + difficultyIncrease;
            int baseDefense = Random.Shared.Next(2, 5) + difficultyIncrease;

            monsters.Add(new Monster(
                name,
                monsterLevel,
                maxHealth,
                baseAttack,
                baseDefense));
        }

        return monsters;
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
}
