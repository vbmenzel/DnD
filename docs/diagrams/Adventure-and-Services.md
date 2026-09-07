# Adventure and Supporting Services

[Back to the readable UML guide](../UML-Overview.md) · [Everything diagram](../UML.md)

> **Canonical diagram:** [UML.md](../UML.md) is authoritative. This focused
> view is provided for readability and may occasionally lag behind it.

```mermaid
classDiagram
direction LR

class Adventure {
    -Party _party
    -IDiceRoller _diceRoller
    +Start()
    -AwardExperience(EncounterResult result)
    -AwardLoot(EncounterResult result)
    -RestorePartyMana()
}

class Encounter {
    +Start() EncounterResult
}

class EncounterResult {
    +bool PartyWon
    +IReadOnlyList~Monster~ DefeatedMonsters
}

class Party {
    +GetMembers() IReadOnlyList~Character~
}

class MonsterGenerator {
    <<static>>
    +Generate(int encounterNumber, IDiceRoller diceRoller) IReadOnlyList~Monster~
}

class LootGenerator {
    <<static>>
    +Generate(IReadOnlyList~Monster~ defeatedMonsters, IDiceRoller diceRoller) Item?
}

class TravelNarrator {
    <<static>>
    +Narrate(IDiceRoller diceRoller)
}

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

class Character
class Monster
class Item

Adventure "0..*" --> "1" Party : manages
Adventure "0..*" --> "1" IDiceRoller : shares randomness
Adventure ..> Encounter : creates and runs
Adventure ..> EncounterResult : processes
Adventure ..> MonsterGenerator : requests monsters
Adventure ..> LootGenerator : requests rewards
Adventure ..> TravelNarrator : requests narration

MonsterGenerator ..> Monster : creates
LootGenerator ..> Monster : reads defeated
LootGenerator ..> Item : returns
EncounterResult "0..*" o-- "0..*" Monster : reports defeated
Party "0..*" o-- "0..*" Character : contains
Character <|-- Monster

MonsterGenerator ..> IDiceRoller
LootGenerator ..> IDiceRoller
TravelNarrator ..> IDiceRoller
IDiceRoller <|.. RandomDiceRoller
IDiceRoller <|.. FixedDiceRoller
```
