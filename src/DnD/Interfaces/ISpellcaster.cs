namespace DnD.Interfaces;

public interface ISpellcaster
{
    int CurrentMana { get; }

    void CastSpell(IDamageable target);
}