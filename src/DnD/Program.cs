using DnD.Characters;
using DnD.Combat.Dice;
using DnD.Game;
using DnD.Interfaces;
using DnD.Parties;

namespace DnD;

/// <summary>
/// Provides the entry point for the application.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Creates the party and starts the adventure.
    /// </summary>
    private static void Main()
    {
        Party party = CreateParty();
        IDiceRoller diceRoller = new RandomDiceRoller();
        var adventure = new Adventure(party, diceRoller);

        adventure.Start();
    }

    /// <summary>
    /// Creates the party participating in the demonstration encounter.
    /// </summary>
    /// <returns>The configured party.</returns>
    private static Party CreateParty()
    {
        // TODO: Replace the predefined party with interactive character creation.
        // New characters have zero XP and empty inventories.
        var party = new Party();
        party.AddMember(new Warrior("Aric", 1, 35, 9, 7));
        party.AddMember(new Rogue("Lyra", 1, 25, 7, 8));
        party.AddMember(new Wizard("Mira", 1, 22, 8, 6));

        return party;
    }
}
