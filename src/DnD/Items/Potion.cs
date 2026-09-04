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

		public void Use(Character target)
		{
			target.Heal(HealAmount);
		}
	}
}