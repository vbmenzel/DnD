using DnD.Characters;
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
    /// <returns>
    /// A randomized item, or <see langword="null"/> when no monsters were
    /// defeated.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="defeatedMonsters"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static Item? Generate(IReadOnlyList<Monster> defeatedMonsters)
    {
        ArgumentNullException.ThrowIfNull(defeatedMonsters);

        if (defeatedMonsters.Count == 0)
        {
            return null;
        }

        int monsterLevel = defeatedMonsters.Max(monster => monster.Level);

        return Random.Shared.Next(LootTypeCount) switch
        {
            0 => CreateWeapon(monsterLevel),
            1 => CreateArmor(monsterLevel),
            _ => CreatePotion(monsterLevel),
        };
    }

    /// <summary>
    /// Creates a weapon scaled to a monster level.
    /// </summary>
    /// <param name="monsterLevel">The level used to scale the weapon.</param>
    /// <returns>The generated weapon.</returns>
    private static Weapon CreateWeapon(int monsterLevel)
    {
        int maximumBonus = 1 + (monsterLevel / 4);
        int damageBonus = Random.Shared.Next(1, maximumBonus + 1);

        return new Weapon($"Weapon (+{damageBonus} damage)", damageBonus);
    }

    /// <summary>
    /// Creates armor scaled to a monster level.
    /// </summary>
    /// <param name="monsterLevel">The level used to scale the armor.</param>
    /// <returns>The generated armor.</returns>
    private static Armor CreateArmor(int monsterLevel)
    {
        int maximumBonus = 1 + (monsterLevel / 4);
        int defenseBonus = Random.Shared.Next(1, maximumBonus + 1);

        return new Armor($"Armor (+{defenseBonus} defense)", defenseBonus);
    }

    /// <summary>
    /// Creates a healing potion scaled to a monster level.
    /// </summary>
    /// <param name="monsterLevel">The level used to scale the potion.</param>
    /// <returns>The generated potion.</returns>
    private static Potion CreatePotion(int monsterLevel)
    {
        int healAmount = Random.Shared.Next(5, 11) + (monsterLevel * 2);

        return new Potion($"Healing potion ({healAmount} HP)", healAmount);
    }
}
