using DnD.Characters;
using DnD.Interfaces;
using DnD.Items;
using DnD.Parties;

namespace DnD.Game;

public static class GameConsole
{
	public static void Start(Party party, IDiceRoller diceRoller)
	{
		bool running = true;

		while (running)
		{
			Console.Clear();

			// Title
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("╔══════════════════════════════════════════════╗");
			Console.WriteLine("║                                              ║");
			Console.WriteLine("║          >>  DUNGEONS & DRAGONS  <<          ║");
			Console.WriteLine("║              THE DARK REALM                  ║");
			Console.WriteLine("║                                              ║");
			Console.WriteLine("╚══════════════════════════════════════════════╝");

			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("              Welcome, adventurer!");
			Console.WriteLine();

			// Menu
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("              [1] >>  Begin Adventure");
			Console.WriteLine("              [2] >> View Party");
			Console.WriteLine("              [3] >> View Inventory");
			Console.WriteLine("              [4] >> How to Play");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("              [5] >> Exit Game");

			Console.ResetColor();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("────────────────────────────────────────────────");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" Choose your path > ");
			Console.ResetColor();

			string? choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					Console.Clear();

					// Display the adventure header
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.WriteLine("╔══════════════════════════════════════════════╗");
					Console.WriteLine("║              THE JOURNEY BEGINS              ║");
					Console.WriteLine("╚══════════════════════════════════════════════╝");
					Console.ResetColor();

					Console.WriteLine();

					// Start a new adventure with the current party
					var adventure = new Adventure(party, diceRoller);
					adventure.Start();

					// Wait before returning to the main menu
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.WriteLine("────────────────────────────────────────────────");

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(" Press ENTER to return to the tavern...");
					Console.ResetColor();

					Console.ReadLine();
					break;

				case "2":
					// Show the current party
					ShowParty(party);
					break;

				case "3":
					// Show the inventory for each party member
					ShowInventory(party);
					break;

				case "4":
					// Display the game instructions
					ShowHowToPlay();
					break;

				case "5":
					Console.Clear();

					// Display the farewell message and close the game
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.WriteLine("╔══════════════════════════════════════════════╗");
					Console.WriteLine("║               JOURNEY'S END                  ║");
					Console.WriteLine("╚══════════════════════════════════════════════╝");

					Console.WriteLine();
					Console.WriteLine(" Your adventure ends... for now.");
					Console.WriteLine(" Farewell, adventurer!");
					Console.ResetColor();

					running = false;
					break;

				default:
					// Handle invalid menu input
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(" That path does not exist.");
					Console.ResetColor();

					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write(" Press ENTER to try again...");
					Console.ResetColor();

					Console.ReadLine();
					break;
			}
		}
	}

	private static void ShowParty(Party party)
	{
		Console.Clear();

		// Display the party header
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("╔══════════════════════════════════════════════╗");
		Console.WriteLine("║                  YOUR PARTY                  ║");
		Console.WriteLine("╚══════════════════════════════════════════════╝");
		Console.ResetColor();

		Console.WriteLine();

		// Display information for each character in the party
		foreach (Character character in party.GetMembers())
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(
				$" {character.Name.ToUpper()} - {character.GetType().Name.ToUpper()}");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($" Level {character.Level}");

			Console.ResetColor();

			// Create a visual health bar with a maximum length of 20
			const int barLength = 20;

			int filledBars = character.MaxHP > 0
				? character.CurrentHealth * barLength / character.MaxHP
				: 0;

			// Make sure the health bar stays within its valid range
			filledBars = Math.Clamp(filledBars, 0, barLength);

			string healthBar =
				new string('█', filledBars) +
				new string('░', barLength - filledBars);

			// Calculate remaining health as a percentage
			double healthPercent = character.MaxHP > 0
				? (double)character.CurrentHealth / character.MaxHP
				: 0;

			Console.Write(" HP  [");

			// Change the health bar color depending on remaining HP
			if (healthPercent > 0.5)
				Console.ForegroundColor = ConsoleColor.Green;
			else if (healthPercent > 0.25)
				Console.ForegroundColor = ConsoleColor.Yellow;
			else
				Console.ForegroundColor = ConsoleColor.Red;

			Console.Write(healthBar);

			Console.ResetColor();
			Console.WriteLine(
				$"] {character.CurrentHealth}/{character.MaxHP}");

			// Display total attack and defense including equipment bonuses
			Console.WriteLine(
				$" ATK {character.BaseAttack + character.DamageBonus}   " +
				$"DEF {character.BaseDefense + character.DefenseBonus}");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("────────────────────────────────────────────────");
			Console.ResetColor();
		}

		// Wait before returning to the main menu
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write(" Press ENTER to return...");
		Console.ResetColor();

		Console.ReadLine();
	}

	private static void ShowInventory(Party party)
	{
		Console.Clear();

		// Display the inventory header
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("╔══════════════════════════════════════════════╗");
		Console.WriteLine("║                PARTY INVENTORY               ║");
		Console.WriteLine("╚══════════════════════════════════════════════╝");
		Console.ResetColor();

		Console.WriteLine();

		// Display the inventory for each character
		foreach (Character character in party.GetMembers())
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(
				$" {character.Name.ToUpper()} - {character.GetType().Name.ToUpper()}");
			Console.ResetColor();

			var items = character.Inventory.GetItems();

			// Show a message if the character has no items
			if (items.Count == 0)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine(" Inventory is empty.");
				Console.ResetColor();
			}
			else
			{
				// Display every item with information based on its type
				foreach (var item in items)
				{
					Console.Write(" > ");

					switch (item)
					{
						case Weapon weapon:
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write($"{weapon.Name}");
							Console.ResetColor();
							Console.WriteLine($"  [Weapon | Damage +{weapon.DamageBonus}]");
							break;

						case Armor armor:
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.Write($"{armor.Name}");
							Console.ResetColor();
							Console.WriteLine($"  [Armor | Defense +{armor.DefenseBonus}]");
							break;

						case Potion potion:
							Console.ForegroundColor = ConsoleColor.Green;
							Console.Write($"{potion.Name}");
							Console.ResetColor();
							Console.WriteLine($"  [Potion | Heal +{potion.HealAmount} HP]");
							break;

						default:
							Console.WriteLine(item.Name);
							break;
					}
				}
			}

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("────────────────────────────────────────────────");
			Console.ResetColor();
		}

		// Wait before returning to the main menu
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write(" Press ENTER to return...");
		Console.ResetColor();

		Console.ReadLine();
	}
	private static void ShowHowToPlay()
	{
		Console.Clear();

		// Display the How to Play header
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("╔══════════════════════════════════════════════╗");
		Console.WriteLine("║                 HOW TO PLAY                  ║");
		Console.WriteLine("╚══════════════════════════════════════════════╝");
		Console.ResetColor();

		Console.WriteLine();

		// Introduction
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(" THE ADVENTURE");
		Console.ResetColor();

		Console.WriteLine(" Guide your party through dangerous encounters,");
		Console.WriteLine(" defeat monsters and collect powerful loot.");

		Console.WriteLine();

		// Explain the basic combat rules
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(" COMBAT");
		Console.ResetColor();

		Console.WriteLine(" > Choose actions by entering their number.");
		Console.WriteLine(" > Choose a target when an action requires one.");
		Console.WriteLine(" > Defeat the monsters before your party falls.");

		Console.WriteLine();

		// Explain progression and items
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(" PROGRESSION");
		Console.ResetColor();

		Console.WriteLine(" > Defeated monsters reward experience.");
		Console.WriteLine(" > Experience can increase your level.");
		Console.WriteLine(" > Find weapons, armor and potions.");

		Console.WriteLine();

		// Display the available character classes
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(" YOUR HEROES");
		Console.ResetColor();

		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write(" WARRIOR");
		Console.ResetColor();
		Console.WriteLine("  - Strong melee fighter");

		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(" ROGUE");
		Console.ResetColor();
		Console.WriteLine("    - Fast physical attacker");

		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.Write(" WIZARD");
		Console.ResetColor();
		Console.WriteLine("   - Uses spells and mana");

		// Wait before returning to the main menu
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.WriteLine("────────────────────────────────────────────────");

		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write(" Press ENTER to return...");
		Console.ResetColor();

		Console.ReadLine();
	}
}