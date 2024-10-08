﻿using System;

static class Inventory
{
    public static List<IItem> ItemList { get; set; } = new List<IItem>();
    public static int ChestKeyCount;

    public static event Action PotionConsumed;

    public static void RemoveItem (ItemId id)
    {
        for (int i = 0; i < ItemList.Count; i++)
            if (ItemList[i].Id == id)
            {
                ItemList.RemoveAt(i);
                break;
            }
    }

    public static int QueryPotionCount(ItemId id)
    {
        int count = 0;
        var list = GetItemsByType<Potion>();
        foreach (Potion item in list)
            count += id == item.Id ? 1 : 0;
        return count;
    }

    public static void UsePotion(ItemId id)
    {
        var potionList = GetItemsByType<Potion>();
        foreach (Potion item in potionList)
        {
            if (id == item.Id)
            {
                item.Consume();
                RemoveItem(item.Id);
                PotionConsumed?.Invoke();
                break;
            }
        }
    }

    public static int QueryItemCount(ItemId id)
    {
        int count = 0;
        foreach (IItem item in ItemList)
            count += id == item.Id ? 1 : 0;
        return count;
    }

    public static IReadOnlyList<T> GetItemsByType<T>()
    {
        return ItemList.OfType<T>().ToList();
    }

    public static IReadOnlyList<IItem> GetItemsSortedByName()
    {
        return GetItemsSortedByName(ItemList);
    }

    public static IReadOnlyList<IItem> GetItemsSortedByPrice()
    {
        return GetItemsSortedByPrice(ItemList);
    }

    public static IReadOnlyList<IItem> GetItemsSortedByName (List<IItem> items)
    {
        return items.OrderBy(item => item.Name).ToList();
    }

    public static IReadOnlyList<IItem> GetItemsSortedByPrice (List<IItem> items)
    {
        return items.OrderBy(item => item.Price).ToList();
    }
}
