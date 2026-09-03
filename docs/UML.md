# UML Class Diagram

```mermaid
classDiagram

class Character {
    <<abstract>>
    +string Name
    +int Level
    +int MaxHealth
    +int CurrentHealth
    +TakeDamage(int amount)
    +Heal(int amount)
    +Attack(Character target)
}

class Warrior {
    +Attack(Character target)
}

class Wizard {
    +int Mana
    +Attack(Character target)
    +CastSpell(Character target)
}

class Rogue {
    +Attack(Character target)
}

Character <|-- Warrior
Character <|-- Wizard
Character <|-- Rogue


class IDamageable {
    <<interface>>
    +int CurrentHealth
    +bool /IsDefeated
    +TakeDamage(int amount)
}

class ISpellcaster {
    <<interface>>
    +int CurrentMana
    +CastSpell(IDamageable target)
}

IDamageable <|.. Character
IDamageable <|.. Monster
ISpellcaster <|.. Wizard


class Monster {
    +string Name
    +int MaxHealth
    +int CurrentHealth
    +TakeDamage(int amount)
    +Attack(Character target)
}


class Party {
    -List~Character~ Members
    +AddMember(Character character)
    +RemoveMember(Character character)
}

Party o-- Character : contains


class Item {
    <<abstract>>
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

Character *-- Item : inventory


class Encounter {
    -Party party
    -List~Monster~ monsters
    -IDiceRoller diceRoller
    +Start()
    +PlayerTurn()
    +MonsterTurn()
}

Encounter --> Party
Encounter --> Monster
Encounter --> IDiceRoller


class IDiceRoller {
    <<interface>>
    +Roll(int sides) int
}

class RandomDiceRoller {
    +Roll(int sides) int
}

class FixedDiceRoller {
    +int FixedValue
    +Roll(int sides) int
}

IDiceRoller <|.. RandomDiceRoller
IDiceRoller <|.. FixedDiceRoller


class CharacterIsDefeatedException {
    +CharacterIsDefeatedException(string message)
}

class InsufficientManaException {
    +InsufficientManaException(string message)
}
