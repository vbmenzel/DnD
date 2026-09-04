using DnD.Characters;
using DnD.Combat.Actions;

namespace DnD.Combat;

/// <summary>
/// Displays combat menus and reads player selections from the console.
/// </summary>
internal static class CombatConsole
{
    /// <summary>
    /// Prompts the player to select one of a character's available actions.
    /// </summary>
    /// <param name="character">The character taking a turn.</param>
    /// <param name="actions">The actions available to the character.</param>
    /// <returns>The selected combat action.</returns>
    public static CombatAction SelectAction(
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
    /// Prompts the player to select one of the available targets.
    /// </summary>
    /// <param name="targets">The characters available as targets.</param>
    /// <returns>The selected character.</returns>
    public static Character SelectTarget(IReadOnlyList<Character> targets)
    {
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
}
