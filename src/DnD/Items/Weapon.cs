namespace DnD.Items
{
	public class Weapon : Item
	{
		public int DamageBonus { get; private set; }

		public bool IsEquipped { get; private set; }

        public Weapon(string name, int damageBonus)
			: base(name)
		{
			DamageBonus = damageBonus;
			IsEquipped = false;
		}

		public void Equip()
        {
            IsEquipped = true;
        }

        public void Unequip()
        {
            IsEquipped = false;
        }
    }
}