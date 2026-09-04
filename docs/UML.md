# UML Class Diagram

```mermaid
classDiagram

class Character {
    <<Abstract>>
    -int BaseExperienceRequirement
    -int ExperienceRequirementIncrease
    +int HP
    +int MaxHP
    +string Name
    +int Level
    +int BaseDefense
    +int Xp
    +int ExperienceRequiredForNextLevel
    +int BaseAttack
    +int CurrentHealth
    +bool IsDefeated
    +int DamageBonus
    +int DefenseBonus
    +Inventory Inventory
    +TakeDamage(int amount)
    +Heal(int amount)
    +GainExperience(int amount)
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
    -int ExperienceRewardPerLevel
    +int ExperienceReward
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
    -Dictionary~EquipmentSlot, Item~ _equippedItems
    +AddItem(Item item)
    +RemoveItem(Item item)
    +GetItems() IReadOnlyList~Item~
    +GetEquippedItem(EquipmentSlot slot) Item?
    -EquipIfUpgrade(Item item)
    -RefreshEquipmentAfterRemoval(Item removedItem)
}

class EquipmentSlot {
    <<enumeration>>
    Weapon
    Armor
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
Inventory --> EquipmentSlot

class Encounter {
    -Party _party
    -List~Monster~ _monsters
    -CombatActionResolver _actionResolver
    +Start() EncounterResult
    +PlayerTurn()
    +MonsterTurn()
}

Encounter "0..*" --> "1" Party
Encounter "0..1" --> "1..*" Monster
Encounter --> CombatActionResolver
Encounter ..> EncounterResult : creates

class EncounterResult {
    +bool PartyWon
    +IReadOnlyList~Monster~ DefeatedMonsters
}

EncounterResult --> Monster

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

class Adventure {
    -Party _party
    -IDiceRoller _diceRoller
    +Start()
    -AwardExperience(EncounterResult result)
    -AwardLoot(EncounterResult result)
    -CalculateExperienceReward(EncounterResult result) int
    -GetLivingPartyMembers() IReadOnlyList~Character~
}

Adventure --> Party
Adventure --> IDiceRoller
Adventure ..> Encounter : creates
Adventure ..> EncounterResult
Adventure ..> MonsterGenerator
Adventure ..> LootGenerator
Adventure ..> TravelNarrator

class MonsterGenerator {
    <<static>>
    -int MaximumMonsterCount
    -int MaximumMonsterLevel
    -string[] MonsterNames
    +Generate(int encounterNumber) IReadOnlyList~Monster~
}

class TravelNarrator {
    <<static>>
    -int MinimumTravelMessages
    -int MaximumTravelMessages
    -int MinimumTravelDelayMilliseconds
    -int MaximumTravelDelayMilliseconds
    -string[] TravelMessages
    +Narrate()
}

MonsterGenerator ..> Monster : creates

class LootGenerator {
    <<static>>
    -int LootTypeCount
    +Generate(IReadOnlyList~Monster~ defeatedMonsters) Item?
    -CreateWeapon(int monsterLevel) Weapon
    -CreateArmor(int monsterLevel) Armor
    -CreatePotion(int monsterLevel) Potion
}

LootGenerator ..> Monster
LootGenerator ..> Weapon : creates
LootGenerator ..> Armor : creates
LootGenerator ..> Potion : creates

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
