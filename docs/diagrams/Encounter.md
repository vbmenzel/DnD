# Encounter Handling

[Back to the readable UML guide](../UML-Overview.md) · [Everything diagram](../UML.md)

> **Canonical diagram:** [UML.md](../UML.md) is authoritative. This focused
> view is provided for readability and may occasionally lag behind it.

```mermaid
classDiagram
direction LR

class Encounter {
    -Party _party
    -List~Monster~ _monsters
    -CombatActionResolver _actionResolver
    +Start() EncounterResult
    +PlayerTurn()
    +MonsterTurn()
    -ResolveAction(Character attacker, CombatAction action, Character target)
}

class Party {
    +GetMembers() IReadOnlyList~Character~
}

class Character {
    +bool IsDefeated
    +GetCombatActions() IReadOnlyList~CombatAction~
}

class Monster

class CombatAction {
    +CombatTargetType TargetType
    +bool RequiresAttackRoll
    +int AttackRollModifier
    +CanTarget(Character target) bool
    +Execute(Character target)
}

class CombatConsole {
    <<static>>
    +SelectAction(Character character, IReadOnlyList~CombatAction~ actions) CombatAction
    +SelectTarget(IReadOnlyList~Character~ targets) Character
}

class CombatActionResolver {
    -IDiceRoller _diceRoller
    +Resolve(Character attacker, CombatAction action, Character target)
}

class IDiceRoller {
    <<interface>>
    +Roll(int sides) int
}

class ISpellcaster {
    <<interface>>
}

class EncounterResult {
    +bool PartyWon
    +IReadOnlyList~Monster~ DefeatedMonsters
}

class CharacterIsDefeatedException
class InsufficientManaException

Encounter "0..*" --> "1" Party : reads members
Encounter "0..*" o-- "1..*" Monster : has opponents
Encounter "0..1" *-- "1" CombatActionResolver : owns
Encounter ..> CombatConsole : asks for choices
Encounter ..> CombatAction : resolves selected
Encounter ..> EncounterResult : creates

Party "0..*" o-- "0..*" Character : contains
Character <|-- Monster
CombatActionResolver "0..*" --> "1" IDiceRoller : retains
CombatActionResolver ..> CharacterIsDefeatedException : throws
CombatConsole ..> ISpellcaster : reads mana
Encounter ..> CharacterIsDefeatedException : catches
Encounter ..> InsufficientManaException : catches
EncounterResult "0..*" o-- "0..*" Monster : reports defeated
```
