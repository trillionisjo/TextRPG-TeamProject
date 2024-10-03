using System;

static class ShopData
{
    public static float Rate => 0.85f;
    public static event Action Purchased;
    public static event Action Sold;

    public static List<IItem> ItemList { get; set; }

    static ShopData()
    {
        ItemList = new List<IItem>();
    }

    public static void Purchase(int index)
    {
        GameData.Player.SpendGold(ItemList[index].Price);
        Inventory.ItemList.Add(ItemList[index]);
        ItemList.RemoveAt(index);

        Purchased?.Invoke();
    }

    public static void Sell(int index)
    {
        IItem item = Inventory.ItemList[index];

        if (item is IEquipable equipable)
        {
            if (EquipManager.IsEquiptedItem(equipable))
                EquipManager.UnequipItem(equipable.Slot);
        }

        int gold = (int)(item.Price * Rate);
        GameData.Player.AddGold(gold);
        ItemList.Add(Inventory.ItemList[index]);
        Inventory.ItemList.RemoveAt(index);

        Sold?.Invoke();
    }
}
