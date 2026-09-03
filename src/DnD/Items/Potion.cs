using DnD.Characters;

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

		// Character kommer fra Person 1.
		// Namespace/import skal tilpasses, når Character er merged.
		public void Use(Character target)
		{
			target.Heal(HealAmount);
		}
	}
}
