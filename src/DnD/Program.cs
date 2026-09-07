using DnD.Characters;
using DnD.Combat.Dice;
using DnD.Game;
using DnD.Interfaces;
using DnD.Parties;

namespace DnD;

/// <summary>
/// Provides the entry point for the application.
/// </summary>
internal static class Program
{
	private static void Main()
	{
		Party party = CreateParty();
		IDiceRoller diceRoller = new RandomDiceRoller();

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
			Console.WriteLine("        [1]  Begin Adventure");
			Console.WriteLine("        [2]  View Party");

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("        [3]  Exit Game");

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

	private static Party CreateParty()
	{
		var party = new Party();

		party.AddMember(new Warrior("Aric", 1, 35, 9, 7));
		party.AddMember(new Rogue("Lyra", 1, 25, 7, 8));
		party.AddMember(new Wizard("Mira", 1, 22, 8, 6));

		return party;
	}

	private static void ShowParty(Party party)
	{
		Console.WriteLine("=== Party ===");
		Console.WriteLine();

		foreach (Character character in party.GetMembers())
		{
			Console.WriteLine(
				$"{character.Name} - {character.GetType().Name} - " +
				$"Level {character.Level} - HP {character.CurrentHealth}/{character.MaxHP}");
		}
	}
}