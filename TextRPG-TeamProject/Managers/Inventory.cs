using System;

static class Inventory
{
    public static List<Item> ItemList { get; set; } = null;

    public static IReadOnlyList<T> GetItemsByType<T>() where T : Item
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
