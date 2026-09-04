# UML Class Diagram

```mermaid
classDiagram

class Character {
    <<Abstract>>
    +int HP
    +int MaxHP
    +string Name
    +int Level
    +int BaseDefense
    +int Xp
    +int BaseAttack
    +int CurrentHealth
    +bool IsDefeated
    +Inventory Inventory
    +TakeDamage(int amount)
    +Heal(int amount)
    +Attack(IDamageable target)
    +GetCombatActions() IReadOnlyList~CombatAction~
    #GetClassCombatActions() IReadOnlyList~CombatAction~
    -UsePotion(Potion potion, Character target)
    +ToString() string
}

class Warrior {
    +Attack(IDamageable target)
    #GetClassCombatActions() IReadOnlyList~CombatAction~
    -HeavyAttack(Character target)
}

class Wizard {
    +Attack(IDamageable target)
    #GetClassCombatActions() IReadOnlyList~CombatAction~
}

class Rogue {
    +Attack(IDamageable target)
    #GetClassCombatActions() IReadOnlyList~CombatAction~
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
    +Attack(IDamageable target)
    #GetClassCombatActions() IReadOnlyList~CombatAction~
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
    +CanTarget(Character target) bool
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
    -List~Character~ members
    +AddMember(Character character)
    +RemoveMember(Character character)
    +GetMembers() IReadOnlyList~Character~
}

Party "0..1" o-- "0..*" Character : contains

class Inventory {
    -List~Item~ items
    +AddItem(Item item)
    +RemoveItem(Item item)
    +GetItems() IReadOnlyList~Item~
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
    -Party _party
    -List~Monster~ _monsters
    -CombatActionResolver _actionResolver
    +Start()
    +PlayerTurn()
    +MonsterTurn()
}

Encounter "0..*" --> "1" Party
Encounter "0..1" --> "1..*" Monster
Encounter --> CombatActionResolver

class CombatActionResolver {
    -IDiceRoller _diceRoller
    -int _attackDieSides
    +Resolve(Character attacker, CombatAction action, Character target)
}

class CombatConsole {
    <<static>>
    +SelectAction(Character character, IReadOnlyList~CombatAction~ actions) CombatAction
    +SelectTarget(IReadOnlyList~Character~ targets) Character
}

Encounter ..> CombatConsole
CombatActionResolver --> IDiceRoller

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
