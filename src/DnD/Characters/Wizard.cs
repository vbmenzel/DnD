using DnD.Combat.Actions;
using DnD.Combat.Exceptions;
using DnD.Interfaces;

namespace DnD.Characters;

/// <summary>
/// Represents a spellcasting character who uses mana for powerful attacks.
/// </summary>
public class Wizard : Character, ISpellcaster
{
    private const int BaseMana = 12;
    private const int ManaPerLevel = 3;
    private const int SpellManaCost = 5;
    private const int SpellDamageBonus = 4;

    /// <summary>
    /// Initializes a new instance of the <see cref="Wizard"/> class.
    /// </summary>
    /// <param name="name">The wizard's name.</param>
    /// <param name="level">The wizard's initial level.</param>
    /// <param name="maxHP">The wizard's maximum health points.</param>
    /// <param name="baseAttack">The wizard's base attack value.</param>
    /// <param name="baseDefense">The wizard's base defense value.</param>
    public Wizard(
        string name,
        int level,
        int maxHP,
        int baseAttack,
        int baseDefense)
        : base(name, level, maxHP, baseAttack, baseDefense)
    {
        CurrentMana = MaxMana;
    }

    /// <summary>
    /// Gets the wizard's current mana points.
    /// </summary>
    public int CurrentMana { get; private set; }

    /// <summary>
    /// Gets the wizard's maximum mana points for the current level.
    /// </summary>
    public int MaxMana => BaseMana + (Level * ManaPerLevel);

    /// <summary>
    /// Attacks a target by casting the wizard's damaging spell.
    /// </summary>
    /// <param name="target">The target receiving the spell damage.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InsufficientManaException">
    /// Thrown when the wizard does not have enough mana to cast the spell.
    /// </exception>
    public override void Attack(IDamageable target)
    {
        CastSpell(target);
    }

    /// <inheritdoc />
    public void CastSpell(IDamageable target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (CurrentMana < SpellManaCost)
        {
            throw new InsufficientManaException(
                $"{Name} does not have enough mana to cast a spell.");
        }

        CurrentMana -= SpellManaCost;

        int damage = Math.Max(
            BaseAttack + DamageBonus + Level + SpellDamageBonus,
            0);

        target.TakeDamage(damage);
        Console.WriteLine(
            $"{Name} casts a spell on {target} for {damage} damage!");
    }

    /// <inheritdoc />
    public void RestoreMana(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Mana restoration cannot be negative.");
        }

        CurrentMana = (int)Math.Min((long)CurrentMana + amount, MaxMana);
    }

    /// <inheritdoc />
    protected override IReadOnlyList<CombatAction> GetClassCombatActions()
    {
        return
        [
            new CombatAction(
                $"Cast spell ({SpellManaCost} mana)",
                CombatTargetType.Enemy,
                false,
                target => Attack(target)),
            new CombatAction(
                "Staff attack",
                CombatTargetType.Enemy,
                true,
                StaffAttack),
        ];
    }

    /// <summary>
    /// Performs a weak attack that does not consume mana.
    /// </summary>
    /// <param name="target">The character receiving the attack.</param>
    private void StaffAttack(Character target)
    {
        int damage = Math.Max((BaseAttack + DamageBonus) / 2, 0);

        target.TakeDamage(damage);
        Console.WriteLine(
            $"{Name} strikes {target} with a staff for {damage} damage!");
    }
}
