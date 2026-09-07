# DnD Console Game

A console-based Dungeons & Dragons inspired adventure game developed in C#.

The project demonstrates object-oriented programming concepts such as
inheritance, polymorphism, interfaces, encapsulation, collections,
custom exceptions and dependency injection.

## Features

- Turn-based combat
- Party with Warrior, Rogue and Wizard
- Different character abilities
- Multiple monsters and target selection
- Experience and level-up system
- Inventory system
- Weapons, armor and potions
- Equipment system using Dictionary
- Dice rolling through IDiceRoller
- RandomDiceRoller and FixedDiceRoller
- Custom exceptions
- Interactive console menu

## How to Play

Run the application and choose an option from the main menu:

1. Begin Adventure
2. View Party
3. View Inventory
4. How to Play
5. Exit Game

During combat, enter the number of the action you want to use and then
choose a target when required.

Defeat monsters to progress through the adventure, earn experience and
collect loot.

## Characters

### Warrior
A strong melee character with access to Heavy Attack.

### Rogue
A fast physical attacker with access to Sneak Attack.

### Wizard
A spellcaster who uses mana to cast spells.

## UML Diagram

The UML diagram shows the structure of the application, including
classes, inheritance, interfaces, relationships and cardinalities.

![DnD UML Diagram](docs/UML.png)

For the complete UML source, see [UML.md](docs/UML.md).

## Project Structure

- `Characters` - Character classes such as Warrior, Rogue, Wizard and Monster
- `Combat` - Combat system, actions, dice and exceptions
- `Game` - Adventure flow, menu, loot and monster generation
- `Interfaces` - IDamageable, ISpellcaster and IDiceRoller
- `Items` - Inventory, weapons, armor and potions
- `Parties` - Party management
- `tests` - Automated tests

## Technologies

- C#
- .NET
- Git / GitHub
- Mermaid UML
