using System;
namespace DnD; 

public interface ISpellcaster
{
    int CurrentMana { get; }

    void CastSpell(IDamageable target);
}
