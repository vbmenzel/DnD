# UML Class Diagram

```mermaid
classDiagram

class Character {
    <<Abstract>>
    +string Name
    +int Level
    +int MaxHealth
    +int CurrentHealth
    +TakeDamage(int amount)
    +Heal(int amount)
    +Attack(Character target)
    +GetCombatActions() IReadOnlyList~CombatAction~
}

class Warrior {
    +Attack(Character target)
    +GetCombatActions() IReadOnlyList~CombatAction~
    -HeavyAttack(Character target)
}

class Wizard {
    +Attack(Character target)
    +GetCombatActions() IReadOnlyList~CombatAction~
}

class Rogue {
    +Attack(Character target)
    +GetCombatActions() IReadOnlyList~CombatAction~
    -SneakAttack(Character target)
}

class IDamageable {
    <<Interface>>
    +int CurrentHealth
    +bool IsDefeated
    +TakeDamage(int amount)
}

class ISpellcaster {
    <<Interface>>
    +int CurrentMana
    +CastSpell(IDamageable target)
}

class Monster {
    +Attack(Character target)
    +GetCombatActions() IReadOnlyList~CombatAction~
}

Character <|-- Warrior
Character <|-- Wizard
Character <|-- Rogue
Character <|-- Monster

Character ..|> IDamageable

class CombatAction {
    +string Name
    +CombatTargetType TargetType
    +bool RequiresAttackRoll
    +int AttackRollModifier
    +Execute(Character target)
}

class CombatTargetType {
    <<enumeration>>
    Enemy
    Ally
    Self
}

Character ..> CombatAction : exposes
CombatAction --> CombatTargetType

class Party {
    -List~Character~ Members
    +AddMember(Character character)
    +RemoveMember(Character character)
}

Party "0..1" o-- "0..*" Character : contains

class Item {
    <<Abstract>>
    +string Name
}

class Weapon {
    +int DamageBonus
}

class Armor {
    +int DefenseBonus
}

class Potion {
    +int HealAmount
    +Use(Character target)
}

Item <|-- Weapon
Item <|-- Armor
Item <|-- Potion

Character "1" *-- "0..*" Item : inventory

class Encounter {
    -Party party
    -List~Monster~ monsters
    -IDiceRoller diceRoller
    +Start()
    +PlayerTurn()
    +MonsterTurn()
}

Encounter "0..*" --> "1" Party
Encounter "0..1" --> "1..*" Monster
Encounter "0..*" --> "1" IDiceRoller

class IDiceRoller {
    <<Interface>>
    +Roll(int sides) int
}

class RandomDiceRoller {
    +Roll(int sides) int
}

class FixedDiceRoller {
    +int FixedValue
    +Roll(int sides) int
}

RandomDiceRoller ..|> IDiceRoller
FixedDiceRoller ..|> IDiceRoller

class CharacterIsDefeatedException {
    +CharacterIsDefeatedException(string message)
}

class InsufficientManaException {
    +InsufficientManaException(string message)
}
