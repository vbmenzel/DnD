# Potential classes or Interfaces

## Submission Documentation

- [Final UML class diagram](docs/UML.md)
- [Design decisions](docs/DesignDecisions.md)

Character - abstract

int HP (get, Private set)

int Max HP (get, Private set)

string Name (get, Private set)

int Level (get, Private set)

int xp (get, Private set)

int AttackPower (get, Private set)

public Attack(IDamageable target)

punlic TakeDamage(int amount)

public Heal(int amount)

internal AddstatusEffect(StatusEffect effect)

protected CalculatelncomingDamage(int amount)

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

public CurrentMana int

public CastSpell(target: IDamageable)

---

IDamageable

public CurrentHealth int public
public (derived)IsDefeated bool

---

IDiceRoller

public Roll(int sides)

---
