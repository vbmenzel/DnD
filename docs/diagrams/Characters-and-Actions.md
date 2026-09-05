# Characters and Combat Actions

[Back to the readable UML guide](../UML-Overview.md) · [Everything diagram](../UML.md)

> **Canonical diagram:** [UML.md](../UML.md) is authoritative. This focused
> view is provided for readability and may occasionally lag behind it.

```mermaid
classDiagram
direction TB

class IDamageable {
    <<interface>>
    +int CurrentHealth
    +bool IsDefeated
    +TakeDamage(int amount)
}

class ISpellcaster {
    <<interface>>
    +int CurrentMana
    +int MaxMana
    +CastSpell(IDamageable target)
    +RestoreMana(int amount)
}

class Character {
    <<abstract>>
    +int HP
    +int MaxHP
    +string Name
    +int Level
    +int Xp
    +int BaseAttack
    +int BaseDefense
    +int DamageBonus
    +int DefenseBonus
    +TakeDamage(int amount)
    +Heal(int amount)
    +GainExperience(int amount)
    +Attack(IDamageable target)*
    +GetCombatActions() IReadOnlyList~CombatAction~
}

class Warrior {
    +Attack(IDamageable target)
    -HeavyAttack(Character target)
}

class Rogue {
    +Attack(IDamageable target)
    -SneakAttack(Character target)
}

class Wizard {
    +int CurrentMana
    +int MaxMana
    +Attack(IDamageable target)
    +CastSpell(IDamageable target)
    +RestoreMana(int amount)
    -StaffAttack(Character target)
}

class Monster {
    +int ExperienceReward
    +Attack(IDamageable target)
}

class CombatAction {
    +string Name
    +CombatTargetType TargetType
    +bool RequiresAttackRoll
    +int AttackRollModifier
    +CanTarget(Character target) bool
    +Execute(Character target)
}

class CombatTargetType {
    <<enumeration>>
    Enemy
    Ally
    Self
}

class InsufficientManaException
class CharacterIsDefeatedException

IDamageable <|.. Character
ISpellcaster ..> IDamageable : targets
Character <|-- Warrior
Character <|-- Rogue
Character <|-- Wizard
Character <|-- Monster
ISpellcaster <|.. Wizard

Character ..> CombatAction : creates and returns
CombatAction ..> Character : targets
CombatAction ..> CombatTargetType : uses
Wizard ..> InsufficientManaException : throws
Monster ..> CharacterIsDefeatedException : throws
```
