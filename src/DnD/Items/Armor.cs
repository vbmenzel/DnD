namespace DnD.Items
{
	public class Armor : Item
	{
		public int DefenseBonus { get; private set; }

		public Armor(string name, int defenseBonus)
			: base(name)
		{
			DefenseBonus = defenseBonus;
		}
	}
}