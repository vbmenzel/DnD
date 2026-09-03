# Potential classes or Interfaces

Character - abstract

int HP (get, Private set)

int Max HP (get, Private set)

string Name (get, Private set)

int Level (get, Private set)

int xp (get, Private set)

int AttackPower (get, Private set)

public Attack(target: IDamageable)

punlic TakeDamage(amount: int)

public Heal(amount: int)

internal AddstatusEffect(effect: StatusEffect)

protected CalculatelncomingDamage(amount: int)

---


Warrior : Character

Wizard : Character

Rogue : Character

Monster : Character

---

Party

Encounter

StatusEffect - abstract 

PoisonEffect : StatusEffect

AttackBoostEffect : StatusEffect

Item - abstract +

Weapon : Item

Armor : Item

Potion : Item


## Interfaces
---

ISpellcaster

public CurrentMana int - public getter private setter 

public CastSpell(target: IDamageable)

---

IDamageable

---

IDiceRoller

---
