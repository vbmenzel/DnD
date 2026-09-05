# Design Decisions

## Access Modifiers

`Character.HP` and `Character.MaxHP` have public getters and private setters.
Other objects need to read a character's health, but health may only change
through the constructor, `TakeDamage`, and `Heal`. This keeps health within the
valid range from zero through maximum health.

The collections inside `Party` and `Inventory` are private. Their public
methods expose controlled operations and read-only views, preventing callers
from replacing the collections or modifying them without using the domain
methods.

`Character.GetClassCombatActions` is protected and abstract. Subclasses must
provide their class-specific actions, but unrelated code cannot call this
extension point directly. Consumers instead use the public `GetCombatActions`
method, which combines class and inventory actions.

Combat dependencies such as the party, monsters, and action resolver are
private and readonly in `Encounter`. They are supplied during construction and
cannot be replaced while an encounter is running.

## Custom Exception

`CharacterIsDefeatedException` represents an invalid domain action: a defeated
character attempting to act. `CombatActionResolver` throws it before executing
the action, and `Encounter` catches it and displays the message. The attempted
turn therefore fails without terminating the application.

## Loose Coupling Through `IDiceRoller`

Game and combat code depend on `IDiceRoller` instead of `System.Random`.
`RandomDiceRoller` supplies normal random results, while `FixedDiceRoller` can
supply predictable results during testing or demonstrations. The caller can
change how rolls are produced without changing encounter, travel, monster, or
loot logic.
