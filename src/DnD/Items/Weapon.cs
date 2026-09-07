namespace DnD.Items
{
	public class Weapon : Item
	{
		public int DamageBonus { get; private set; }

		public Weapon(string name, int damageBonus)
			: base(name)
		{
			DamageBonus = damageBonus;
		}
	}
}