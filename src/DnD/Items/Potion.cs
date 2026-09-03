namespace DnD.Items
{
	public class Potion : Item
	{
		public int HealAmount { get; private set; }

		public Potion(string name, int healAmount)
			: base(name)
		{
			HealAmount = healAmount;
		}

		// TODO: Tilføj Use(Character target), når Character fra Person 1 er merged.
		// Use skal kalde target.Heal(HealAmount).
	}
}