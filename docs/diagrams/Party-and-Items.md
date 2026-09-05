# Party, Inventory, and Items

[Back to the readable UML guide](../UML-Overview.md) · [Everything diagram](../UML.md)

> **Canonical diagram:** [UML.md](../UML.md) is authoritative. This focused
> view is provided for readability and may occasionally lag behind it.

```mermaid
classDiagram
direction TB

class Party {
    -List~Character~ members
    +AddMember(Character character)
    +RemoveMember(Character character)
    +GetMembers() IReadOnlyList~Character~
}

class Character {
    +string Name
    +int DamageBonus
    +int DefenseBonus
    +Inventory Inventory
    +Heal(int amount)
}

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

Party "0..*" o-- "0..*" Character : contains
Character "0..1" *-- "1" Inventory : owns
Inventory "0..*" o-- "0..*" Item : contains
Inventory ..> EquipmentSlot : uses as key

Item <|-- Weapon
Item <|-- Armor
Item <|-- Potion
Potion ..> Character : heals
```
