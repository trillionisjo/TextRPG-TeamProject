using System;
using System.Numerics;

static class GameData
{
    static public Player Player = new Player();
    static public List<Monster> AliveMonster;
    static public List<Monster> DeathMonster;
    static public int DungeonLv = 0;

    public static void InitDatas()
    {
        if(DungeonLv == 0)
            { DungeonLv = 1; }

        Console.CursorVisible = false;


        // Test
        GameData.Player.AddGold(20000);

        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.TraineesArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.IronArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.WornSword));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.BronzeAxe));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanSpear));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));

        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.TraineesArmor));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.IronArmor));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanArmor));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.WornSword));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.BronzeAxe));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanSpear));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));
    }
    
}
