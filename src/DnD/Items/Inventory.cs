namespace DnD.Items
{
	// Inventory holder styr på en Characters items.
	// Listen er privat, så items kun ændres gennem Inventory-metoderne.
	public class Inventory
	{
		private readonly List<Item> items = new();
		private readonly Dictionary<EquipmentSlot, Item> _equippedItems = new();

		public void AddItem(Item item)
		{
			items.Add(item);
			EquipIfUpgrade(item);
		}

		public void RemoveItem(Item item)
		{
			if (!items.Remove(item))
			{
				return;
			}

			RefreshEquipmentAfterRemoval(item);
		}

		public IReadOnlyList<Item> GetItems()
		{
			return items.AsReadOnly();
		}

		/// <summary>
		/// Gets the item currently assigned to an equipment slot.
		/// </summary>
		/// <param name="slot">The equipment slot to look up.</param>
		/// <returns>
		/// The equipped item, or <see langword="null"/> when the slot is empty.
		/// </returns>
		public Item? GetEquippedItem(EquipmentSlot slot)
		{
			return _equippedItems.GetValueOrDefault(slot);
		}

		/// <summary>
		/// Equips an item when it is stronger than the item currently occupying
		/// the corresponding slot.
		/// </summary>
		/// <param name="item">The item to consider for equipment.</param>
		private void EquipIfUpgrade(Item item)
		{
			switch (item)
			{
				case Weapon weapon when
					GetEquippedItem(EquipmentSlot.Weapon) is not Weapon equippedWeapon ||
					weapon.DamageBonus > equippedWeapon.DamageBonus:
					_equippedItems[EquipmentSlot.Weapon] = weapon;
					break;

				case Armor armor when
					GetEquippedItem(EquipmentSlot.Armor) is not Armor equippedArmor ||
					armor.DefenseBonus > equippedArmor.DefenseBonus:
					_equippedItems[EquipmentSlot.Armor] = armor;
					break;
			}
		}

		/// <summary>
		/// Replaces removed equipment with the strongest remaining item for its
		/// slot.
		/// </summary>
		/// <param name="removedItem">The item removed from the inventory.</param>
		private void RefreshEquipmentAfterRemoval(Item removedItem)
		{
			EquipmentSlot? removedSlot = removedItem switch
			{
				Weapon => EquipmentSlot.Weapon,
				Armor => EquipmentSlot.Armor,
				_ => null,
			};

			if (removedSlot is not EquipmentSlot slot ||
				!ReferenceEquals(GetEquippedItem(slot), removedItem))
			{
				return;
			}

			_equippedItems.Remove(slot);

			foreach (Item item in items)
			{
				EquipIfUpgrade(item);
			}
		}
	}
}
