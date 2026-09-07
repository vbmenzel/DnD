using DnD.Characters;
using DnD.Interfaces;
using DnD.Items;

namespace DnD.Game;

/// <summary>
/// Generates randomized item rewards from defeated monsters.
/// </summary>
internal static class LootGenerator
{
    private const int LootTypeCount = 3;

    /// <summary>
    /// Generates one item scaled to the strongest defeated monster.
    /// </summary>
    /// <param name="defeatedMonsters">
    /// The monsters defeated during an encounter.
    /// </param>
    /// <param name="diceRoller">The dice roller used for random selections.</param>
    /// <returns>
    /// A randomized item, or <see langword="null"/> when no monsters were
    /// defeated.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="defeatedMonsters"/> is
    /// <see langword="null"/>, or when <paramref name="diceRoller"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static Item? Generate(
        IReadOnlyList<Monster> defeatedMonsters,
        IDiceRoller diceRoller)
    {
        ArgumentNullException.ThrowIfNull(defeatedMonsters);
        ArgumentNullException.ThrowIfNull(diceRoller);

        if (defeatedMonsters.Count == 0)
        {
            return null;
        }

        int monsterLevel = defeatedMonsters.Max(monster => monster.Level);

        return (diceRoller.Roll(LootTypeCount) - 1) switch
        {
            0 => CreateWeapon(monsterLevel, diceRoller),
            1 => CreateArmor(monsterLevel, diceRoller),
            _ => CreatePotion(monsterLevel, diceRoller),
        };
    }

    /// <summary>
    /// Creates a weapon scaled to a monster level.
    /// </summary>
    /// <param name="monsterLevel">The level used to scale the weapon.</param>
    /// <param name="diceRoller">The dice roller used to select its bonus.</param>
    /// <returns>The generated weapon.</returns>
    private static Weapon CreateWeapon(
        int monsterLevel,
        IDiceRoller diceRoller)
    {
        int maximumBonus = 1 + (monsterLevel / 4);
        int damageBonus = diceRoller.Roll(maximumBonus);

        return new Weapon($"Weapon (+{damageBonus} damage)", damageBonus);
    }

    /// <summary>
    /// Creates armor scaled to a monster level.
    /// </summary>
    /// <param name="monsterLevel">The level used to scale the armor.</param>
    /// <param name="diceRoller">The dice roller used to select its bonus.</param>
    /// <returns>The generated armor.</returns>
    private static Armor CreateArmor(
        int monsterLevel,
        IDiceRoller diceRoller)
    {
        int maximumBonus = 1 + (monsterLevel / 4);
        int defenseBonus = diceRoller.Roll(maximumBonus);

        return new Armor($"Armor (+{defenseBonus} defense)", defenseBonus);
    }

    /// <summary>
    /// Creates a healing potion scaled to a monster level.
    /// </summary>
    /// <param name="monsterLevel">The level used to scale the potion.</param>
    /// <param name="diceRoller">
    /// The dice roller used to select its healing amount.
    /// </param>
    /// <returns>The generated potion.</returns>
    private static Potion CreatePotion(
        int monsterLevel,
        IDiceRoller diceRoller)
    {
        int healAmount = diceRoller.Roll(6) + 4 + (monsterLevel * 2);

        return new Potion($"Healing potion ({healAmount} HP)", healAmount);
    }
}
