using DnD.Characters;

namespace DnD.Combat;

/// <summary>
/// Describes the outcome of a completed combat encounter.
/// </summary>
public sealed class EncounterResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EncounterResult"/> class.
    /// </summary>
    /// <param name="partyWon">
    /// A value indicating whether the party defeated every monster.
    /// </param>
    /// <param name="defeatedMonsters">
    /// The monsters defeated during the encounter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="defeatedMonsters"/> is
    /// <see langword="null"/>.
    /// </exception>
    internal EncounterResult(
        bool partyWon,
        IEnumerable<Monster> defeatedMonsters)
    {
        ArgumentNullException.ThrowIfNull(defeatedMonsters);

        PartyWon = partyWon;
        DefeatedMonsters = defeatedMonsters.ToList().AsReadOnly();
    }

    /// <summary>
    /// Gets a value indicating whether the party won the encounter.
    /// </summary>
    public bool PartyWon { get; }

    /// <summary>
    /// Gets the monsters defeated during the encounter.
    /// </summary>
    public IReadOnlyList<Monster> DefeatedMonsters { get; }
}
