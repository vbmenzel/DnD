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
                TravelNarrator.Narrate();
            }

            IReadOnlyList<Monster> monsters = MonsterGenerator.Generate(
                encounterNumber);
            var encounter = new Encounter(_party, monsters, _diceRoller);

            Console.WriteLine();
            Console.WriteLine($"Encounter {encounterNumber} begins!");
            EncounterResult result = encounter.Start();

            // TODO: Award XP and dropped items after every encounter.

            if (!result.PartyWon)
            {
                Console.WriteLine("The adventure has come to an end.");
                return;
            }

            encounterNumber++;
        }
    }

}
