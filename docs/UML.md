# UML Class Diagram

```mermaid
classDiagram

class Character {
    <<Abstract>>
    +string Name
    +int Level
    +int MaxHealth
    +int CurrentHealth
    +int BaseDefense
    +Inventory Inventory
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
}

Character <|-- Warrior
Character <|-- Wizard
Character <|-- Rogue
Character <|-- Monster

Character ..|> IDamageable
Wizard ..|> ISpellcaster

class Party {
    -List~Character~ Members
    +AddMember(Character character)
    +RemoveMember(Character character)
}

Party "0..1" o-- "0..*" Character : contains

class Inventory {
    -List~Item~ Items
    +AddItem(Item item)
    +RemoveItem(Item item)
}

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

Character "1" *-- "1" Inventory : owns
Inventory "1" o-- "0..*" Item : contains

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

```

Character and Inventory use composition because the Inventory belongs to the
Character and does not exist independently. Inventory and Item use aggregation
because Items can exist independently and can be transferred between
inventories.
