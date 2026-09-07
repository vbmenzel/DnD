using DnD.Characters;
using DnD.Interfaces;

namespace DnD.Game;

/// <summary>
/// Generates randomized groups of monsters for adventure encounters.
/// </summary>
internal static class MonsterGenerator
{
    private const int MaximumMonsterCount = 3;
    private const int MaximumMonsterLevel = 20;

    private static readonly string[] MonsterNames =
    [
        "Bandit",
        "Giant rat",
        "Goblin",
        "Skeleton",
        "Wolf",
        "Young orc",
    ];

    /// <summary>
    /// Generates a randomized monster group scaled to an encounter number.
    /// </summary>
    /// <param name="encounterNumber">The one-based encounter number.</param>
    /// <param name="diceRoller">The dice roller used for random selections.</param>
    /// <returns>The monsters participating in the encounter.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="encounterNumber"/> is less than one.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="diceRoller"/> is <see langword="null"/>.
    /// </exception>
    public static IReadOnlyList<Monster> Generate(
        int encounterNumber,
        IDiceRoller diceRoller)
    {
        ArgumentNullException.ThrowIfNull(diceRoller);

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
        int monsterCount = diceRoller.Roll(maximumEncounterSize);

        // Select without replacement to avoid duplicate names in one encounter.
        var availableNames = MonsterNames.ToList();
        var monsters = new List<Monster>();

        for (int monsterIndex = 0; monsterIndex < monsterCount; monsterIndex++)
        {
            int nameIndex = diceRoller.Roll(availableNames.Count) - 1;
            string name = availableNames[nameIndex];
            availableNames.RemoveAt(nameIndex);

            // Random base values vary monsters within the same difficulty tier.
            int maxHealth = diceRoller.Roll(6) + 6 + (difficultyIncrease * 3);
            int baseAttack = diceRoller.Roll(3) + 1 + difficultyIncrease;
            int baseDefense = diceRoller.Roll(3) + 1 + difficultyIncrease;

            monsters.Add(new Monster(
                name,
                monsterLevel,
                maxHealth,
                baseAttack,
                baseDefense));
        }

        return monsters;
    }
}
