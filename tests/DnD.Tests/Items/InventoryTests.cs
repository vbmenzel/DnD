using DnD.Items;

namespace DnD.Tests.Items;

/// <summary>
/// Contains tests for inventory equipment-slot behavior.
/// </summary>
public class InventoryTests
{
    /// <summary>
    /// Verifies that adding a weapon equips it in the weapon slot.
    /// </summary>
    [Fact]
    public void AddItemWithWeaponEquipsWeapon()
    {
        var inventory = new Inventory();
        var weapon = new Weapon("Sword", 2);

        inventory.AddItem(weapon);

        Assert.Same(
            weapon,
            inventory.GetEquippedItem(EquipmentSlot.Weapon));
    }

    /// <summary>
    /// Verifies that stronger equipment replaces weaker equipment.
    /// </summary>
    [Fact]
    public void AddItemWithStrongerWeaponReplacesEquippedWeapon()
    {
        var inventory = new Inventory();
        var weakWeapon = new Weapon("Dagger", 1);
        var strongWeapon = new Weapon("Sword", 3);
        inventory.AddItem(weakWeapon);

        inventory.AddItem(strongWeapon);

        Assert.Same(
            strongWeapon,
            inventory.GetEquippedItem(EquipmentSlot.Weapon));
    }

    /// <summary>
    /// Verifies that weaker equipment does not replace stronger equipment.
    /// </summary>
    [Fact]
    public void AddItemWithWeakerWeaponKeepsEquippedWeapon()
    {
        var inventory = new Inventory();
        var strongWeapon = new Weapon("Sword", 3);
        var weakWeapon = new Weapon("Dagger", 1);
        inventory.AddItem(strongWeapon);

        inventory.AddItem(weakWeapon);

        Assert.Same(
            strongWeapon,
            inventory.GetEquippedItem(EquipmentSlot.Weapon));
    }

    /// <summary>
    /// Verifies that removing equipped gear selects the best replacement.
    /// </summary>
    [Fact]
    public void RemoveItemWithEquippedWeaponEquipsBestRemainingWeapon()
    {
        var inventory = new Inventory();
        var weakWeapon = new Weapon("Dagger", 1);
        var strongWeapon = new Weapon("Sword", 3);
        inventory.AddItem(weakWeapon);
        inventory.AddItem(strongWeapon);

        inventory.RemoveItem(strongWeapon);

        Assert.Same(
            weakWeapon,
            inventory.GetEquippedItem(EquipmentSlot.Weapon));
    }

    /// <summary>
    /// Verifies that weapon and armor slots are stored independently.
    /// </summary>
    [Fact]
    public void AddItemsWithWeaponAndArmorEquipsBothItems()
    {
        var inventory = new Inventory();
        var weapon = new Weapon("Sword", 2);
        var armor = new Armor("Chain mail", 3);

        inventory.AddItem(weapon);
        inventory.AddItem(armor);

        Assert.Same(
            weapon,
            inventory.GetEquippedItem(EquipmentSlot.Weapon));
        Assert.Same(
            armor,
            inventory.GetEquippedItem(EquipmentSlot.Armor));
    }
}
