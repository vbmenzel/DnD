# Readable UML Guide

This page gives a high-level view of the application. The detailed diagrams
below each cover one connected part of the system, while the
[Everything diagram](UML.md) keeps the complete model in one place.

> **Canonical diagram:** [UML.md](UML.md) is the authoritative version. These
> smaller views are maintained for readability and may occasionally lag behind
> it. If the diagrams disagree, follow `UML.md`.

```mermaid
flowchart LR
    subgraph Entry[Application entry]
        Program
    end

    subgraph Game[Adventure loop]
        Adventure
        MonsterGenerator[Monster generator]
        LootGenerator[Loot generator]
        TravelNarrator[Travel narrator]
    end

    subgraph Combat[Encounter handling]
        Encounter
        CombatConsole[Combat console]
        ActionResolver[Action resolver]
    end

    subgraph Domain[Characters and equipment]
        Party
        Characters
        Inventory
        Items
    end

    Dice[Dice roller]

    Program -.->|creates and starts| Adventure
    Adventure -.->|creates and runs| Encounter
    Adventure -.->|calls| MonsterGenerator
    Adventure -.->|calls| LootGenerator
    Adventure -.->|calls| TravelNarrator
    Adventure -->|retains| Party

    Encounter -.->|requests input| CombatConsole
    Encounter -->|owns| ActionResolver
    Encounter -->|retains| Party

    Party -->|contains| Characters
    Characters -->|own| Inventory
    Inventory -->|contains| Items
    MonsterGenerator -.->|creates monsters| Characters
    LootGenerator -.->|creates| Items

    Adventure -->|retains| Dice
    MonsterGenerator -.->|uses| Dice
    LootGenerator -.->|uses| Dice
    TravelNarrator -.->|uses| Dice
    ActionResolver -->|retains| Dice
```

## Focused diagrams

- [Characters and combat actions](diagrams/Characters-and-Actions.md) - inheritance, health, mana, and available actions
- [Party, inventory, and items](diagrams/Party-and-Items.md) - ownership, equipment, weapons, armor, and potions
- [Encounter handling](diagrams/Encounter.md) - turn selection, target selection, action resolution, and encounter results
- [Adventure and supporting services](diagrams/Adventure-and-Services.md) - the game loop, generation, rewards, travel, and dice implementations
