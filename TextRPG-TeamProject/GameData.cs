using System;
using System.Numerics;

static class GameData
{
    public static Player Player = new Knight();

    public static void InitDatas ()
    {
        Player.Name = "김말";

        // Test
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.TraineesArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.IronArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.WornSword));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.BronzeAxe));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanSpear));
    }
}
