namespace DnD.Items
{
	// Inventory holder styr på en Characters items.
	// Listen er privat, så items kun ændres gennem Inventory-metoderne.
	public class Inventory
	{
		private readonly List<Item> items = new();

		public void AddItem(Item item)
		{
			items.Add(item);
		}

		public void RemoveItem(Item item)
		{
			items.Remove(item);
		}

		public IReadOnlyList<Item> GetItems()
		{
			return items.AsReadOnly();
		}
	}
}