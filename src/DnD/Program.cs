using DnD.Characters;
using DnD.Combat;
using DnD.Combat.Dice;
using DnD.Interfaces;
using DnD.Parties;

namespace DnD;

/// <summary>
/// Provides the entry point for the application.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Creates the combatants and starts a combat encounter.
    /// </summary>
    private static void Main()
    {
        Party party = CreateParty();
        IReadOnlyList<Monster> monsters = CreateMonsters();
        IDiceRoller diceRoller = new RandomDiceRoller();
        var encounter = new Encounter(party, monsters, diceRoller);

        Console.WriteLine("The encounter begins!");
        encounter.Start();
    }

    /// <summary>
    /// Creates the party participating in the demonstration encounter.
    /// </summary>
    /// <returns>The configured party.</returns>
    private static Party CreateParty()
    {
        // TODO: Replace the predefined party with interactive character creation.
        var party = new Party();
        party.AddMember(new Warrior("Aric", 1, 35, 9, 7));
        party.AddMember(new Rogue("Lyra", 1, 25, 7, 8));
        party.AddMember(new Wizard("Mira", 1, 22, 8, 6));

        return party;
    }

    /// <summary>
    /// Creates the monsters participating in the demonstration encounter.
    /// </summary>
    /// <returns>The configured monsters.</returns>
    private static IReadOnlyList<Monster> CreateMonsters()
    {
        return
        [
            new Monster("Goblin", 2, 16, 5, 5),
            new Monster("Orc", 3, 28, 7, 7),
        ];
    }
}
