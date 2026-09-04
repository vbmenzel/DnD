using System;

public interface ISpellcaster
{
    int CurrentMana { get; }

    void CastSpell(IDamageable target);
}
