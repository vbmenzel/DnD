using DnD.Interfaces;

namespace DnD.Game;

/// <summary>
/// Displays randomized travel narration between encounters.
/// </summary>
internal static class TravelNarrator
{
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

    /// <summary>
    /// Displays a short, randomized journey and pauses before the next
    /// encounter.
    /// </summary>
    /// <param name="diceRoller">The dice roller used for random selections.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="diceRoller"/> is <see langword="null"/>.
    /// </exception>
    public static void Narrate(IDiceRoller diceRoller)
    {
        ArgumentNullException.ThrowIfNull(diceRoller);

        // Select from a copy to avoid repeating a message during one journey.
        var availableMessages = TravelMessages.ToList();
        int messageCount = MinimumTravelMessages + diceRoller.Roll(
            MaximumTravelMessages - MinimumTravelMessages + 1) - 1;

        Console.WriteLine();
        Console.WriteLine("The party continues its journey...");

        for (int messageIndex = 0;
             messageIndex < messageCount;
             messageIndex++)
        {
            Delay(diceRoller);

            int selectedIndex = diceRoller.Roll(availableMessages.Count) - 1;
            Console.WriteLine(availableMessages[selectedIndex]);
            availableMessages.RemoveAt(selectedIndex);
        }

        // Pause once more before the next encounter begins.
        Delay(diceRoller);
    }

    /// <summary>
    /// Waits for a short randomized travel delay.
    /// </summary>
    /// <param name="diceRoller">The dice roller used to select the delay.</param>
    private static void Delay(IDiceRoller diceRoller)
    {
        int delayMilliseconds = MinimumTravelDelayMilliseconds +
            diceRoller.Roll(
                MaximumTravelDelayMilliseconds -
                MinimumTravelDelayMilliseconds + 1) - 1;

        Thread.Sleep(delayMilliseconds);
    }
}
