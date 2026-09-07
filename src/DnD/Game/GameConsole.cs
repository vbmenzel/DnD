using DnD.Characters;
using DnD.Interfaces;
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

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("========================================");
			Console.WriteLine("        ⚔  DUNGEONS & DRAGONS  ⚔");
			Console.WriteLine("========================================");

			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("        Welcome, adventurer!");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("        [1]  Begin Adventure");
			Console.WriteLine("        [2]  View Party");
			Console.WriteLine("        [3]  View Inventory");
			Console.WriteLine("        [4]  How to Play");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("        [5]  Exit Game");

			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine("----------------------------------------");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Choose your path: ");
			Console.ResetColor();

			string? choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					Console.Clear();

					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.WriteLine("========================================");
					Console.WriteLine("          THE JOURNEY BEGINS");
					Console.WriteLine("========================================");
					Console.ResetColor();
					Console.WriteLine();

					var adventure = new Adventure(party, diceRoller);
					adventure.Start();

					Console.WriteLine();
					Console.WriteLine("----------------------------------------");

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("Press ENTER to return to the tavern...");
					Console.ResetColor();

					Console.ReadLine();
					break;

				case "2":
					Console.Clear();

					ShowParty(party);

					Console.WriteLine();
					Console.WriteLine("----------------------------------------");

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("Press ENTER to return...");
					Console.ResetColor();

					Console.ReadLine();
					break;

				case "3":
					Console.Clear();

					ShowInventory(party);

					Console.WriteLine();
					Console.WriteLine("----------------------------------------");

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("Press ENTER to return...");
					Console.ResetColor();

					Console.ReadLine();
					break;

				case "4":
					Console.Clear();

					ShowHowToPlay();

					Console.WriteLine();
					Console.WriteLine("----------------------------------------");

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("Press ENTER to return...");
					Console.ResetColor();

					Console.ReadLine();
					break;

				case "5":
					Console.Clear();

					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.WriteLine();
					Console.WriteLine("Your adventure ends... for now.");
					Console.WriteLine("Farewell, adventurer!");
					Console.ResetColor();

					running = false;
					break;

				default:
					Console.WriteLine();

					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("That path does not exist.");
					Console.ResetColor();

					Console.WriteLine("Press ENTER to try again...");
					Console.ReadLine();
					break;
			}
		}
	}

	private static void ShowParty(Party party)
	{
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("========================================");
		Console.WriteLine("                 PARTY");
		Console.WriteLine("========================================");
		Console.ResetColor();

		Console.WriteLine();

		foreach (Character character in party.GetMembers())
		{
			Console.WriteLine(
				$"{character.Name} - {character.GetType().Name} - " +
				$"Level {character.Level} - " +
				$"HP {character.CurrentHealth}/{character.MaxHP}");
		}
	}

	private static void ShowInventory(Party party)
	{
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("========================================");
		Console.WriteLine("            PARTY INVENTORY");
		Console.WriteLine("========================================");
		Console.ResetColor();

		Console.WriteLine();

		foreach (Character character in party.GetMembers())
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"{character.Name} - {character.GetType().Name}");
			Console.ResetColor();

			var items = character.Inventory.GetItems();

			if (items.Count == 0)
			{
				Console.WriteLine("  Inventory is empty.");
			}
			else
			{
				foreach (var item in items)
				{
					Console.WriteLine($"  - {item.Name}");
				}
			}

			Console.WriteLine();
		}
	}
	private static void ShowHowToPlay()
	{
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("========================================");
		Console.WriteLine("              HOW TO PLAY");
		Console.WriteLine("========================================");
		Console.ResetColor();

		Console.WriteLine();
		Console.WriteLine("Guide your party through dangerous encounters.");
		Console.WriteLine();
		Console.WriteLine("- Choose actions by entering their number.");
		Console.WriteLine("- Defeat the monsters before your party falls.");
		Console.WriteLine("- Each character has different abilities.");
		Console.WriteLine("- Defeated monsters reward experience.");
		Console.WriteLine("- Experience can increase your level.");
		Console.WriteLine("- You can find weapons, armor and potions.");
		Console.WriteLine();
		Console.WriteLine("Warrior - strong melee fighter");
		Console.WriteLine("Rogue   - fast physical attacker");
		Console.WriteLine("Wizard  - uses spells and mana");
	}
}