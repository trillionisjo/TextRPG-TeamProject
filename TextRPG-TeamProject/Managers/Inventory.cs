using System;

static class Inventory
{
    public static List<Item> ItemList { get; set; } = new List<Item>();

    public static void RemoveItem (Item item)
    {
        for (int i = 0; i < ItemList.Count; i++)
            if (ItemList[i].Id == item.Id)
                ItemList.RemoveAt(i);
    }

    public static IReadOnlyList<T> GetItemsByType<T>()
    {
        return ItemList.OfType<T>().ToList();
    }

    public static IReadOnlyList<Item> GetItemsSortedByName()
    {
        return GetItemsSortedByName(ItemList);
    }

    public static IReadOnlyList<Item> GetItemsSortedByPrice()
    {
        return GetItemsSortedByPrice(ItemList);
    }

    public static IReadOnlyList<Item> GetItemsSortedByName (List<Item> items)
    {
        return items.OrderBy(item => item.Name).ToList();
    }

    public static IReadOnlyList<Item> GetItemsSortedByPrice (List<Item> items)
    {
        return items.OrderBy(item => item.Price).ToList();
    }
}
